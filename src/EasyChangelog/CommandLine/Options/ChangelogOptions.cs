using CommandLine;

namespace EasyChangelog.CommandLine.Options
{
    [Verb("changelog")]
    public class ChangelogOptions : IOptionsBase
    {
        public string WorkingDirectory { get; set; }

        [Option('d', "dry-run", Required = false, HelpText = "Dry-run mode is print changelog only. Default: True")]
        public bool DryRun { get; set; }


        [Option('p', "push", Required = false, HelpText = "Push changes to git")]
        public bool Push { get; set; }

        [Option('t', "token", Required = false, HelpText = "Git credential token")]
        public string GitToken { get; set; }

    }
}
