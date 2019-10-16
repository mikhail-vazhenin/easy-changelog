using System.Collections.Generic;
using System.Linq;
using EasyChangelog.Core.Interfaces;
using EasyChangelog.Services.Interfaces;
using EasyChangelog.Tools;
using LibGit2Sharp;
using Version = System.Version;

namespace EasyChangelog.Services
{
    /// <summary>
    /// Version manager
    /// </summary>
    public class VersionService : IVersionService
    {
        private readonly IVersionControl _versionControl;
        private readonly IVersionIncrementStrategy _versionIncrementStrategy;

        public VersionService(IVersionControl versionControl, IVersionIncrementStrategy versionIncrementStrategy)
        {
            _versionControl = versionControl;
            _versionIncrementStrategy = versionIncrementStrategy;
        }

        public Version GetLastReleaseVersion()
        {
            return _versionControl.GetLastReleaseVersion();
        }

        public Version GetNextReleaseVersion()
        {
            var lastVersion = _versionControl.GetLastReleaseVersion();
            var commitsInVersion = _versionControl.GetCommits(lastVersion);

            return _versionIncrementStrategy.NextVersion(lastVersion, commitsInVersion);
        }
    }
}
