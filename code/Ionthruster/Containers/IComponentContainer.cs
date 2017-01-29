using System;

namespace Ionthruster.Containers
{
    public interface IComponentContainer : IDisposable
    {
        TComponent Resolve<TComponent>() where TComponent : class;

        TComponent Resolve<TComponent>(Type componentType) where TComponent : class;
    }
}