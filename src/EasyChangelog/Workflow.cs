using System;
using System.Collections.Generic;
using System.Text;
using EasyChangelog.Configuration;
using EasyChangelog.Modules.Interfaces;

namespace EasyChangelog
{
    public class Workflow
    {
        private readonly IVersionModule _versionModule;
        private readonly Options _options;

        public Workflow(IVersionModule versionModule, Options options)
        {
            _versionModule = versionModule;
            _options = options;
        }


        public void Run()
        {
            if (_options.CurrentVersion)
            {
                var lastVersion = _versionModule.GetLastReleaseVersion();
                Console.WriteLine(lastVersion);
            }

            if (_options.NextVersion)
            {
                var nextVersion = _versionModule.GetNextReleaseVersion();
                Console.WriteLine(nextVersion);
            }
        }
    }
}
