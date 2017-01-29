using Ionthruster.Containers;
using System;
using System.Reflection;

namespace Ionthruster.Tasks
{
    public class PipelineFactory
    {
        private IComponentContainerFactory ContainerFactory { get; }

        public PipelineFactory() : this(new AutofacComponentContainerFactory()) { }

        public PipelineFactory(IComponentContainerFactory containerFactory)
        {
            if (containerFactory == null) throw new ArgumentNullException(nameof(containerFactory));

            ContainerFactory = containerFactory;
        }

        public PipelineScope Create()
        {
            return Create(Assembly.GetCallingAssembly());
        }

        public PipelineScope Create(Assembly moduleAssembly)
        {
            return new PipelineScope(ContainerFactory, moduleAssembly);
        }
    }
}
