using System;
using EasyChangelog.CommandLine.Options;
using EasyChangelog.Services.Interfaces;
using Version = System.Version;

namespace EasyChangelog.CommandLine.Commands
{
    public class ChangelogCommand
    {
        private readonly IChangelogService _changelogService;
        private readonly IVersionService _versionService;

        public ChangelogCommand(IChangelogService changelogService, IVersionService versionService)
        {
            _changelogService = changelogService;
            _versionService = versionService;
        }


        public void Run(ChangelogOptions changelogOptions)
        {
            var currentVersion = _versionService.GetLastReleaseVersion();
            var nextVersion = _versionService.GetNextReleaseVersion();

            var saveToFile = !changelogOptions.DryRun;

            Console.WriteLine(_changelogService.GetChangelog(changelogOptions.WorkingDirectory, currentVersion, nextVersion, saveToFile));

            if (!changelogOptions.DryRun)
            {
                if (changelogOptions.Push)
                {
                    _changelogService.CommitChanges(changelogOptions.WorkingDirectory, nextVersion, changelogOptions.GitToken);
                }
            }
        }
    }
}
