using Autofac;
using Ionthruster.Config;
using Ionthruster.Infrastructure;
using Ionthruster.Instrumentation;
using Ionthruster.Middleware;
using System;
using System.Linq;

namespace Ionthruster.Modules
{
    public class BuildModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<Func<DateTime>>(c => () => DateTime.Now);
            builder.Register<Action<string>>(c => message => Console.WriteLine(message));
            builder.RegisterType<BuildConfig>().As<IBuildConfig>();
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<NugetPackageFinder>().As<INugetPackageFinder>();
            builder.RegisterType<ProcessRunner>().As<IProcessRunner>();
            builder.RegisterType<GitVersionTask>();
        }
    }
}
