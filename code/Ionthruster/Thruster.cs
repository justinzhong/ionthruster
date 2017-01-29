using Ionthruster.Middleware;
using Ionthruster.Tasks;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Ionthruster
{
    public static class Thruster
    {
        public static async Task StartAsync(Func<IPipelineScope, Task> runner)
        {
            await StartAsync(runner, Assembly.GetExecutingAssembly());
        }

        public static async Task StartAsync(Func<IPipelineScope, Task> runner, Assembly moduleAssembly)
        {
            using (var scope = new PipelineFactory().Create(moduleAssembly))
            {
                await runner(scope);
            }
        }
    }
}