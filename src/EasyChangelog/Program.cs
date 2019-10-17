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

            Parser.Default.ParseArguments<ChangelogOptions, CurrentVersionOptions, NextVersionOptions>(args)
                    .WithParsed<CurrentVersionOptions>(versionOptions =>
                       DependencyRegistration.BuildServiceProvider(versionOptions)
                       .GetService<CurrentVersionCommand>()
                       .Run(versionOptions))
                    .WithParsed<NextVersionOptions>(versionOptions =>
                       DependencyRegistration.BuildServiceProvider(versionOptions)
                       .GetService<NextVersionCommand>()
                       .Run(versionOptions))
                    .WithParsed<ChangelogOptions>(changelogOptions =>
                       DependencyRegistration.BuildServiceProvider(changelogOptions)
                       .GetService<ChangelogCommand>()
                       .Run(changelogOptions))
                    .WithNotParsed((errs) => Console.Write(string.Join(Environment.NewLine, errs.Select(e => e.ToString()))));

        }
    }
}
