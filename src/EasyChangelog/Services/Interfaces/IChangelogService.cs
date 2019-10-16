using System;
using System.Collections.Generic;
using EasyChangelog.Core.Models;

namespace EasyChangelog.Services.Interfaces
{
    public interface IChangelogService
    {
        void SaveChangelog(string workingDir);
        string GetChangelogText(string workingDir);
    }
}