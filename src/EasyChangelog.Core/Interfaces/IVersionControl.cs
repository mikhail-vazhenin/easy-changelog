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
        void AddVersionTag(string sha, Version nextVersion);
        string PushChangelog(string changelogFullName, Version nextVersion, string token);
    }
}
