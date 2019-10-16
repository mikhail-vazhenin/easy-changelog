using CommandLine;

namespace EasyChangelog.CommandLine.Options
{
    public interface IOptionsBase
    {
        [Option('w', "working-dir", Required = true, HelpText = "Set path to git project folder")]
        string WorkingDirectory { get; set; }
    }
}
