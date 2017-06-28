using Autofac;
using Ionthruster.Modules;
using System;

namespace Ionthruster.Cli
{
    public static class AutofacConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());
            builder.RegisterIonMiddlewares();
            builder.RegisterIonTasks();

            return builder.Build();
        }
    }
}
