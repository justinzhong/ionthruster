using Autofac;
using System;
using System.Reflection;

namespace Ionthruster.Containers
{
    public class AutofacComponentContainerFactory : IComponentContainerFactory
    {
        public IComponentContainer Create<TAssemblyType>()
        {
            return Create(typeof(TAssemblyType).Assembly);
        }

        public IComponentContainer Create(Assembly moduleAssembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(moduleAssembly);

            var container = builder.Build();
            var scope = container.BeginLifetimeScope();

            return new AutofacComponentContainer(scope);
        }
    }
}