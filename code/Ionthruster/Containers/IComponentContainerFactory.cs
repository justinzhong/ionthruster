using System.Reflection;

namespace Ionthruster.Containers
{
    public interface IComponentContainerFactory
    {
        IComponentContainer Create(params Assembly[] moduleAssemblies);
    }
}