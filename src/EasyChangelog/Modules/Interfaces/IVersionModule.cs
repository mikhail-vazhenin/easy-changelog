using System;

namespace EasyChangelog.Modules.Interfaces
{
    public interface IVersionModule
    {
        Version GetLastReleaseVersion();
        Version GetNextReleaseVersion();
    }
}