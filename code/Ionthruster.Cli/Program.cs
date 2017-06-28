using Ionthruster.Config;
using Ionthruster.Middleware;
using Ionthruster.Tasks;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => Thruster.StartAsync(Run)).Wait();

            Console.ReadKey();
        }

        private static async Task Run(IPipelineScope scope)
        {
            var buildConfig = new BuildConfig();

            // User defined pipeline starts here
            await scope.Start<GitVersionTask>(buildConfig.ProjectPath)
                .Flush();
        }
    }
}
