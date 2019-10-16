using System;
using System.Linq;
using CommandLine;
using EasyChangelog.CommandLine.Commands;
using EasyChangelog.CommandLine.Options;
using Microsoft.Extensions.DependencyInjection;

namespace EasyChangelog
{
    static class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<VersionOptions, ChangelogOptions>(args)
                 .WithParsed<VersionOptions>(versionOptions =>
                    DependencyRegistration.BuildServiceProvider(versionOptions)
                    .GetService<VersionCommand>()
                    .Run(versionOptions))
                 .WithParsed<ChangelogOptions>(changelogOptions =>
                    DependencyRegistration.BuildServiceProvider(changelogOptions)
                    .GetService<ChangelogCommand>()
                    .Run(changelogOptions))
                 .WithNotParsed((errs) => throw new AggregateException(errs.Select(e => new Exception(e.ToString()))));
        }
    }
}
