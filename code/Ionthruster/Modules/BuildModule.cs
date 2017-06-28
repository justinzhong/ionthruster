using Autofac;
using Ionthruster.Config;
using Ionthruster.Infrastructure;
using Ionthruster.Tasks;

namespace Ionthruster.Modules
{
    public class BuildModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BuildConfig>().As<IBuildConfig>();
            builder.RegisterType<NugetPackageFinder>().As<INugetPackageFinder>();
            builder.RegisterType<ProcessRunner>().As<IProcessRunner>();
            builder.RegisterType<GitVersionTask>();
        }
    }
}
