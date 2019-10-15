using System.Collections.Generic;
using System.Linq;
using EasyChangelog.Configuration;
using EasyChangelog.Modules.Interfaces;
using EasyChangelog.Tools;
using LibGit2Sharp;
using Version = System.Version;

namespace EasyChangelog.Modules
{
    public class GitVersionModule : IVersionModule
    {
        private readonly IRepository _repository;
        private readonly ConventionalCommitParser _conventionalCommitParser;

        public GitVersionModule(IRepository repository, ConventionalCommitParser conventionalCommitParser, Options options)
        {
            _repository = repository;
            _conventionalCommitParser = conventionalCommitParser;
        }

        public Version GetLastReleaseVersion()
        {
            var lastVersionTag = GetLastReleaseTag();
            return ParseVersion(lastVersionTag);
        }

        public Version GetNextReleaseVersion()
        {
            var lastVersionTag = GetLastReleaseTag();
            var commitsInVersion = GetCommitsSinceLastVersion(lastVersionTag);

            var conventionalCommits = _conventionalCommitParser.Parse(commitsInVersion);

            var versionIncrement = VersionIncrementStrategy.CreateFrom(conventionalCommits);

            return versionIncrement.NextVersion(ParseVersion(lastVersionTag));
        }

        private ICollection<Commit> GetCommitsSinceLastVersion(Tag versionTag)
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
            Version.TryParse(name.Substring(1, name.Length - 1), out Version version);
            return version;
        }
    }
}
