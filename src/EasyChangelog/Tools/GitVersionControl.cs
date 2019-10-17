using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyChangelog.Core.Interfaces;
using EasyChangelog.Core.Models;
using EasyChangelog.Services.Interfaces;
using LibGit2Sharp;
using Version = System.Version;

namespace EasyChangelog.Tools
{
    public class GitVersionControl : IVersionControl
    {
        private readonly IRepository _repository;
        private readonly ICommitConvention _commitConvention;

        public GitVersionControl(IRepository repository, ICommitConvention commitConvention)
        {
            _repository = repository;
            _commitConvention = commitConvention;
        }

        public ICollection<ConventionalCommit> GetCommits(Version fromVersion)
        {
            var versionTag = GetLastReleaseTag();
            var commitsInVersion = GetCommitsSinceVersion(versionTag);

            return _commitConvention.Parse(commitsInVersion, _repository.Config);
        }

        public Version GetLastReleaseVersion()
        {
            var lastVersionTag = GetLastReleaseTag();
            return ParseVersion(lastVersionTag);
        }

        public string PushChangelog(string changelogFullName, Version nextVersion, string token)
        {
            Commands.Stage(_repository, changelogFullName);

            var author = _repository.Config.BuildSignature(DateTime.Now);
            var committer = author;

            var releaseCommitMessage = $"chore(release): {nextVersion}";
            var commit = _repository.Commit(releaseCommitMessage, author, committer);

            var pushOptions = new PushOptions()
            {
                CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                {
                    Username = "changelog-builder",
                    Password = token
                }
            };

            var branch = _repository.Branches.FirstOrDefault(b => b.FriendlyName.Equals("master", StringComparison.InvariantCultureIgnoreCase));

            _repository.Network.Push(branch, pushOptions);
            return commit.Sha;
        }

        public void AddVersionTag(string sha, Version nextVersion)
        {
            var versionCommit = _repository.Commits.FirstOrDefault(c => c.Sha.Equals(sha, StringComparison.InvariantCultureIgnoreCase));
            var author = _repository.Config.BuildSignature(DateTime.Now);
            var committer = author;

            _repository.Tags.Add($"v{nextVersion}", versionCommit, author, $"{nextVersion}");
        }

        private ICollection<Commit> GetCommitsSinceVersion(Tag versionTag)
        {
            if (versionTag == null)
            {
                return _repository.Commits.ToList();
            }

            var filter = new CommitFilter()
            {
                ExcludeReachableFrom = versionTag
            };

            return _repository.Commits.QueryBy(filter).ToList();
        }

        protected Tag GetLastReleaseTag()
        {
            return _repository.Tags.OrderBy(ParseVersion).Last();
        }

        protected Version ParseVersion(Tag tag)
        {
            var name = tag.FriendlyName;
            if (Version.TryParse(name.Substring(1, name.Length - 1), out Version version)) return version;
            else return new Version(1, 0, 0);

        }


    }
}
