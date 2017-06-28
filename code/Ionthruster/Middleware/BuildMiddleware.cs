using Ionthruster.Config;
using Ionthruster.Infrastructure;
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
        private IBuildAgent BuildAgent { get; }

        public CleanMiddleware(IBuildConfig config, IBuildAgent buildAgent)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (buildAgent == null) throw new ArgumentNullException(nameof(buildAgent));

            Config = config;
            BuildAgent = buildAgent;
        }

        public async Task Run(IPipelineScope scope)
        {
            await BuildAgent.Build(Config.ProjectPath, "/t:Clean");
        }
    }
}
