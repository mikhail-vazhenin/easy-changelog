using CommandLine;

namespace EasyChangelog.Configuration
{
    public class Options
    {
        //[Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        //public bool Verbose { get; set; }

        [Option('w', "working-dir", Required = false, HelpText = "Set path to git project folder")]
        public string WorkingDirectory { get; set; }

        //[Option('d', "dry-run", Required = false, HelpText = "Dry-run mode is print changelog only. Default: True")]
        //public bool DryRun { get; set; } = true;

        [Option("get-version", Required = false, HelpText = "Print only current version")]
        public bool CurrentVersion { get; set; }

        [Option("next-version", Required = false, HelpText = "Print only next version")]
        public bool NextVersion { get; set; }
    }
}
