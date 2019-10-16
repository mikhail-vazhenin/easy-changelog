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

        public Version GetLastReleaseVersion()
        {
            var lastVersionTag = GetLastReleaseTag();
            return ParseVersion(lastVersionTag);
        }

        protected Tag GetLastReleaseTag()
        {
            return _repository.Tags.OrderBy(ParseVersion).Last();
        }

        protected Version ParseVersion(Tag tag)
        {
            var name = tag.FriendlyName;
            Version.TryParse(name.Substring(1, name.Length - 1), out Version version);
            return version;
        }
    }
}
