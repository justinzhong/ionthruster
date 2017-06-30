using Autofac;
using Ionthruster.Modules;

namespace Ionthruster.Cli
{
    public class IonModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ModulesRegistrar.RegisterMiddlewares(builder);
            ModulesRegistrar.RegisterTasks(builder);
        }
    }
}
