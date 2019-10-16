using System;

namespace EasyChangelog.Services.Interfaces
{
    public interface IVersionService
    {
        Version GetLastReleaseVersion();
        Version GetNextReleaseVersion();
    }
}