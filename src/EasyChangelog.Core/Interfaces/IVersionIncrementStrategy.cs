using System;
using System.Collections.Generic;
using EasyChangelog.Core.Models;

namespace EasyChangelog.Core.Interfaces
{
    public interface IVersionIncrementStrategy
    {
        Version NextVersion(Version lastVersion, ICollection<ConventionalCommit> conventionalCommits);
    }
}