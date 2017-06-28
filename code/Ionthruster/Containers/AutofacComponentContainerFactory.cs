using Autofac;
using System.Reflection;

namespace Ionthruster.Containers
{
    public class AutofacComponentContainerFactory : IComponentContainerFactory
    {
        public IComponentContainer Create(params Assembly[] moduleAssemblies)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(moduleAssemblies);

            var container = builder.Build();

            return new AutofacComponentContainer(container);
        }
    }
}