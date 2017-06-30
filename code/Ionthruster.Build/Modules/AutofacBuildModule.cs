using Autofac;
using Ionthruster.Build.Infrastructure;

namespace Ionthruster.Build.Modules
{
    public class AutofacBuildModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NugetPackageFinder>().As<INugetPackageFinder>();
            builder.RegisterType<ProcessRunner>().As<IProcessRunner>();
            builder.RegisterType<MsBuildAgent>().As<IBuildAgent>();
        }
    }
}
