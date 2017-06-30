using Autofac;
using System;

namespace Ionthruster.Containers
{
    public sealed class AutofacComponentContainer : IComponentContainer
    {
        private bool Disposed { get; set; }
        private ILifetimeScope Scope { get; }

        public AutofacComponentContainer(ILifetimeScope scope)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));

            Scope = scope;
        }

        public void Dispose()
        {
            if (Disposed) return;

            Scope.Dispose();
            Disposed = true;
        }

        public TComponent Resolve<TComponent>()
            where TComponent : class
        {
            return Scope.Resolve<TComponent>();
        }

        public TComponent Resolve<TComponent>(Type componentType)
            where TComponent : class
        {
            return (TComponent)Scope.Resolve(componentType);
        }
    }
}
