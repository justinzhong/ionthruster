using Ionthruster.Instrumentation;
using Ionthruster.Middleware.Build.Config;
using Ionthruster.Middleware.Build.Tasks;
using Ionthruster.Pipeline;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Ionthruster.Middleware.Build
{
    [Description(@"Performs the build and package of a .NET project")]
    public class BuildMiddleware : IMiddleware
    {
        private ProjectConfig Config { get; }
        private ILogger Logger { get; }

        public BuildMiddleware(ProjectConfig config, ILogger logger)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            Config = config;
            Logger = logger;
        }

        public async Task Run(IPipelineScope scope)
        {
            await Logger.Log($@"OutputPath: {Config.OutputPath}");
            await Logger.Log($@"ProjectPath: {Config.ProjectPath}");

            // Specify the Build tasks here
            await scope.Start<CleanProjectTask>()
                .Join(Config.ProjectPath, container => container.Resolve<GitVersionTask>())
                .Flush();
        }
    }
}
