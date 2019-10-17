using System;
using EasyChangelog.CommandLine.Options;
using EasyChangelog.Services.Interfaces;

namespace EasyChangelog.CommandLine.Commands
{
    public class NextVersionCommand
    {
        private readonly IVersionService _versionModule;

        public NextVersionCommand(IVersionService versionModule)
        {
            _versionModule = versionModule;
        }

        public void Run(VersionOptions versionOptions)
        {
            var nextVersion = _versionModule.GetNextReleaseVersion();
            Console.WriteLine(nextVersion);
        }
    }
}
