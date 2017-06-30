using Autofac;
using System.Reflection;

namespace Ionthruster.Containers
{
    public class AutofacComponentContainerFactory : IComponentContainerFactory
    {
        public IComponentContainer Create<TAssemblyType>()
        {
            return Create(typeof(TAssemblyType).Assembly);
        }

        public IComponentContainer Create(params Assembly[] moduleAssemblies)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(moduleAssemblies);

            var container = builder.Build();
            var scope = container.BeginLifetimeScope();

            return new AutofacComponentContainer(scope);
        }
    }
}