using System;
using EasyChangelog.CommandLine.Options;
using EasyChangelog.Services.Interfaces;

namespace EasyChangelog.CommandLine.Commands
{
    public class CurrentVersionCommand
    {
        private readonly IVersionService _versionModule;

        public CurrentVersionCommand(IVersionService versionModule)
        {
            _versionModule = versionModule;
        }


        public void Run(CurrentVersionOptions versionOptions)
        {
            var lastVersion = _versionModule.GetLastReleaseVersion();
            Console.WriteLine(lastVersion);
        }
    }
}
