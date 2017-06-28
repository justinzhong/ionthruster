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
        public async Task Run(IPipelineScope scope)
        {
            var buildConfig = new BuildConfig();

            await scope.StartMiddleware<CleanMiddleware>();

            // Specifying the Build tasks here
            await scope.Start<GitVersionTask>(buildConfig.ProjectPath)
                .Flush();
        }
    }

    [Description(@"Cleans the build artefacts of a .NET project")]
    public class CleanMiddleware : IMiddleware
    {
        private IBuildConfig Config { get; }

        public CleanMiddleware(IBuildConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            Config = config;
        }

        public async Task Run(IPipelineScope scope)
        {
            await scope.Start<string, CleanTask, bool>(Config.ProjectPath)
                .Flush();
        }
    }

    public class CleanTask : ITask<string, bool>
    {
        public Task<bool> Run(string projectPath)
        {
            return Task.FromResult(true);
        }
    }
}
