using CommandLine;

namespace EasyChangelog.CommandLine.Options
{
    [Verb("changelog")]
    public class ChangelogOptions : IOptionsBase
    {
        public string WorkingDirectory { get; set; }

        [Option('d', "dry-run", Required = false, HelpText = "Dry-run mode is print changelog only. Default: True")]
        public bool DryRun { get; set; }
    }
}
