using CommandLine;

namespace EasyChangelog.CommandLine.Options
{
    [Verb("version")]
    public class VersionOptions : IOptionsBase
    {
        //[Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        //public bool Verbose { get; set; }

        public string WorkingDirectory { get; set; }

        //[Option('d', "dry-run", Required = false, HelpText = "Dry-run mode is print changelog only. Default: True")]
        //public bool DryRun { get; set; } = true;

        [Option("current", Required = false, HelpText = "Print only current version")]
        public bool Current { get; set; }

        [Option("next", Required = false, HelpText = "Print only next version")]
        public bool Next { get; set; }
    }
}
