using Ionthruster.Config;
using Ionthruster.Pipeline;
using Ionthruster.Tasks;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Ionthruster.Middleware
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
