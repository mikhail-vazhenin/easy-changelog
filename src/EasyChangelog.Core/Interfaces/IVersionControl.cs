using System;
using System.Collections.Generic;
using System.Text;
using EasyChangelog.Core.Models;

namespace EasyChangelog.Core.Interfaces
{
    public interface IVersionControl
    {
        ICollection<ConventionalCommit> GetCommits(Version fromVersion);
        Version GetLastReleaseVersion();
        string PushChangelog(string changelogFullName, Version nextVersion, string token);
    }
}
