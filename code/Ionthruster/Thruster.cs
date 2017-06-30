using Ionthruster.Containers;
using Ionthruster.Instrumentation;
using Ionthruster.Middleware;
using Ionthruster.Pipeline;
using System;
using System.Threading.Tasks;

namespace Ionthruster
{
    public static class Thruster
    {
        public static async Task Start<TMiddleware>()
            where TMiddleware : IMiddleware
        {
            var moduleAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var containerFactory = new AutofacComponentContainerFactory();

            using (var container = containerFactory.Create(moduleAssemblies))
            {
                await Start<TMiddleware>(container);
            }
        }

        public static async Task Start<TMiddleware>(IComponentContainerFactory containerFactory)
        {
            if (containerFactory == null) throw new ArgumentNullException(nameof(containerFactory));

            var moduleAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            using (var container = containerFactory.Create(moduleAssemblies))
            {
                await Start<TMiddleware>(container);
            }
        }

        public static async Task Start<TMiddleware>(IComponentContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            using (var scope = new PipelineScope(container, container.Resolve<ITaskDelegateWrapper>()))
            {
                try
                {
                    var middleware = container.Resolve<IMiddleware>(typeof(TMiddleware));

                    await middleware.Run(scope);
                }
                catch (Exception ex)
                {
                    await container.Resolve<ILogger>().Log(ex.ToString());

                    throw;
                }
            }
        }

        public static async Task Start(Func<IPipelineScope, Task> runner)
        {
            if (runner == null) throw new ArgumentNullException(nameof(runner));

            var moduleAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var containerFactory = new AutofacComponentContainerFactory();

            using (var container = containerFactory.Create(moduleAssemblies))
            {
                await Start(runner, container);
            }
        }

        public static async Task Start(Func<IPipelineScope, Task> runner, IComponentContainerFactory containerFactory)
        {
            if (runner == null) throw new ArgumentNullException(nameof(runner));
            if (containerFactory == null) throw new ArgumentNullException(nameof(containerFactory));

            var moduleAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            using (var container = containerFactory.Create(moduleAssemblies))
            {
                await Start(runner, container);
            }
        }

        public static async Task Start(Func<IPipelineScope, Task> runner, IComponentContainer container)
        {
            if (runner == null) throw new ArgumentNullException(nameof(runner));
            if (container == null) throw new ArgumentNullException(nameof(container));

            using (var scope = new PipelineScope(container, container.Resolve<ITaskDelegateWrapper>()))
            {
                try
                {
                    await runner(scope);
                }
                catch (Exception ex)
                {
                    await container.Resolve<ILogger>().Log(ex.ToString());

                    throw;
                }
            }
        }
    }
}