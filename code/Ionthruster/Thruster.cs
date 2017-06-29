using Ionthruster.Containers;
using Ionthruster.Middleware;
using Ionthruster.Pipeline;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Ionthruster
{
    public static class Thruster
    {
        public static async Task Start<TMiddleware>()
            where TMiddleware : IMiddleware
        {
            var moduleAssembly = Assembly.GetExecutingAssembly();
            var containerFactory = new AutofacComponentContainerFactory();

            await Start<TMiddleware>(containerFactory, moduleAssembly);
        }

        public static async Task Start<TMiddleware>(IComponentContainerFactory containerFactory)
        {
            var moduleAssembly = Assembly.GetExecutingAssembly();

            await Start<TMiddleware>(containerFactory, moduleAssembly);
        }

        public static async Task Start<TMiddleware>(IComponentContainerFactory containerFactory, Assembly moduleAssembly)
        {
            using (var container = containerFactory.Create(moduleAssembly))
            using (var scope = new PipelineScope(container, container.Resolve<ITaskDelegateWrapper>()))
            {
                var middleware = container.Resolve<IMiddleware>(typeof(TMiddleware));
                await middleware.Run(scope);
            }
        }

        public static async Task Start(Func<IPipelineScope, Task> runner)
        {
            await Start(runner, new AutofacComponentContainerFactory(), Assembly.GetExecutingAssembly());
        }

        public static async Task Start(Func<IPipelineScope, Task> runner, IComponentContainerFactory containerFactory)
        {
            await Start(runner, containerFactory, Assembly.GetExecutingAssembly());
        }

        public static async Task Start(Func<IPipelineScope, Task> runner, IComponentContainerFactory containerFactory, Assembly moduleAssembly)
        {
            using (var container = containerFactory.Create(moduleAssembly))
            using (var scope = new PipelineScope(container, container.Resolve<ITaskDelegateWrapper>()))
            {
                await runner(scope);
            }
        }
    }
}