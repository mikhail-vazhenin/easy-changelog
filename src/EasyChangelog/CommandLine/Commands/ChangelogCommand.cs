using System;
using EasyChangelog.CommandLine.Options;
using EasyChangelog.Services.Interfaces;

namespace EasyChangelog.CommandLine.Commands
{
    public class ChangelogCommand
    {
        private readonly IChangelogService _changelogService;

        public ChangelogCommand(IChangelogService changelogService)
        {
            _changelogService = changelogService;
        }


        public void Run(ChangelogOptions changelogOptions)
        {
            Console.WriteLine(_changelogService.GetChangelogText(changelogOptions.WorkingDirectory));

            if (!changelogOptions.DryRun)
            {
                _changelogService.SaveChangelog(changelogOptions.WorkingDirectory);
            }
        }
    }
}
