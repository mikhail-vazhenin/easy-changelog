using System;
using System.Collections.Generic;
using System.Text;
using EasyChangelog.Configuration;
using EasyChangelog.Modules;
using EasyChangelog.Modules.Interfaces;
using EasyChangelog.Tools;
using LibGit2Sharp;
using Microsoft.Extensions.DependencyInjection;

namespace EasyChangelog
{
    public static class DependencyRegistration
    {
        private static readonly IServiceCollection _serviceCollection = new ServiceCollection();


        private static IServiceCollection RegisterDependencies(this IServiceCollection services, Options options)
        {
            services.AddSingleton(s => options);

            services.AddTransient<Workflow>();
            services.AddTransient<IVersionModule, GitVersionModule>();

            services.AddTransient<IRepository, Repository>(_ => new Repository(options.WorkingDirectory));

            services.AddTransient<ConventionalCommitParser>();
            return services;
        }

        public static IServiceProvider BuildServiceProvider(Options options)
        {
            return _serviceCollection.RegisterDependencies(options).BuildServiceProvider();
        }
    }
}
