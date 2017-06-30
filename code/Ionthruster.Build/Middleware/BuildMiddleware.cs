using Ionthruster.Build.Config;
using Ionthruster.Build.Tasks;
using Ionthruster.Middleware;
using Ionthruster.Pipeline;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Ionthruster.Build.Middleware
{
    [Description(@"Performs the build and package of a .NET project")]
    public class BuildMiddleware : IMiddleware
    {
        private ProjectConfig Config { get; }

        public BuildMiddleware(ProjectConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            Config = config;
        }

        public async Task Run(IPipelineScope scope)
        {
            // Specify the Build tasks here
            await scope.Start<CleanProjectTask>()
                .Join(Config.ProjectPath, container => container.Resolve<GitVersionTask>())
                .Flush();
        }
    }
}
