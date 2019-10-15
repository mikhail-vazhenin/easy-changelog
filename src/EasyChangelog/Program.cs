using System;
using System.Linq;
using CommandLine;
using EasyChangelog.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyChangelog
{
    static class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                 .WithParsed(o =>
                 {
                     DependencyRegistration.BuildServiceProvider(o).GetService<Workflow>().Run();

                     //if (o.Verbose)
                     //{
                     //    Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                     //    Console.WriteLine("Quick Start Example! App is in Verbose mode!");
                     //}
                     //else
                     //{
                     //    Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                     //    Console.WriteLine("Quick Start Example!");
                     //}
                 })
                 .WithNotParsed<Options>((errs) => throw new AggregateException(errs.Select(e => new Exception(e.ToString()))));
        }
    }
}
