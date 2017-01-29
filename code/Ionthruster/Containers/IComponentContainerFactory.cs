using System.Reflection;

namespace Ionthruster.Containers
{
    public interface IComponentContainerFactory
    {
        IComponentContainer Create<TAssemblyType>();

        IComponentContainer Create(Assembly moduleAssembly);
    }
}