using System;
using EasyChangelog.CommandLine.Commands;
using EasyChangelog.CommandLine.Options;
using EasyChangelog.Core.Interfaces;
using EasyChangelog.Services;
using EasyChangelog.Services.Interfaces;
using EasyChangelog.Tools;
using LibGit2Sharp;
using Microsoft.Extensions.DependencyInjection;

namespace EasyChangelog
{
    public static class DependencyRegistration
    {
        private static readonly IServiceCollection _serviceCollection = new ServiceCollection();


        private static IServiceCollection RegisterDependencies(this IServiceCollection services, IOptionsBase options)
        {
            services.AddTransient<CurrentVersionCommand>();
            services.AddTransient<NextVersionCommand>();
            services.AddTransient<ChangelogCommand>();

            services.AddTransient<IVersionService, VersionService>();
            services.AddTransient<IChangelogService, ChangelogService>();


            services.AddTransient<IRepository, Repository>(_ => new Repository(options.WorkingDirectory));
            
            services.AddTransient<ICommitConvention, AngularCommitConvention>();
            services.AddTransient<IVersionControl, GitVersionControl>();
            services.AddTransient<IVersionIncrementStrategy, VersionIncrementStrategy>();
            
            return services;
        }

        public static IServiceProvider BuildServiceProvider(IOptionsBase options)
        {
            return _serviceCollection.RegisterDependencies(options).BuildServiceProvider();
        }
    }
}
