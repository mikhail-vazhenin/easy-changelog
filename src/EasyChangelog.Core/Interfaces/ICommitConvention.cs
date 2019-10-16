using System;
using System.Collections.Generic;
using System.Text;
using EasyChangelog.Core.Models;
using LibGit2Sharp;

namespace EasyChangelog.Core.Interfaces
{
    public interface ICommitConvention
    {
        ICollection<ConventionalCommit> Parse(ICollection<Commit> commits, Configuration config);
        ConventionalCommit Parse(Commit commit, Configuration config);
    }
}
