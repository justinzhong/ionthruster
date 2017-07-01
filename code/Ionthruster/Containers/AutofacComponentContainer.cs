using Autofac;
using System;

namespace Ionthruster.Containers
{
    public sealed class AutofacComponentContainer : IComponentContainer
    {
        private bool Disposed { get; set; }
        private IContainer Container { get; }

        public AutofacComponentContainer(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            Container = container;
        }

        public void Dispose()
        {
            if (Disposed) return;

            Container.Dispose();
            Disposed = true;
        }

        public TComponent Resolve<TComponent>()
            where TComponent : class
        {
            return Container.Resolve<TComponent>();
        }

        public TComponent Resolve<TComponent>(Type componentType)
            where TComponent : class
        {
            return (TComponent)Container.Resolve(componentType);
        }
    }
}
