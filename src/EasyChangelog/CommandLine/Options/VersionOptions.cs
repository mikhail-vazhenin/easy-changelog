using CommandLine;

namespace EasyChangelog.CommandLine.Options
{
    public abstract class VersionOptions : IOptionsBase
    {
        public string WorkingDirectory { get; set; }
    }

    [Verb("current-version")]
    public class CurrentVersionOptions : VersionOptions { }

    [Verb("next-version")]
    public class NextVersionOptions : VersionOptions { }
}
