using System;
using System.Collections.Generic;
using EasyChangelog.Core.Models;

namespace EasyChangelog.Services.Interfaces
{
    public interface IChangelogService
    {
        string GetChangelog(string workingDir, Version fromVersion, Version nextVersion, bool saveToFile = false);
        void CommitChanges(string workingDir, Version nextVersion, string token);
    }
}