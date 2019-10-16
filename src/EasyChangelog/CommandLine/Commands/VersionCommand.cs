using System;
using EasyChangelog.CommandLine.Options;
using EasyChangelog.Services.Interfaces;

namespace EasyChangelog.CommandLine.Commands
{
    public class VersionCommand
    {
        private readonly IVersionService _versionModule;

        public VersionCommand(IVersionService versionModule)
        {
            _versionModule = versionModule;
        }


        public void Run(VersionOptions versionOptions)
        {
            if (versionOptions.Current)
            {
                var lastVersion = _versionModule.GetLastReleaseVersion();
                Console.WriteLine(lastVersion);
            }

            if (versionOptions.Next)
            {
                var nextVersion = _versionModule.GetNextReleaseVersion();
                Console.WriteLine(nextVersion);
            }
        }
    }
}
