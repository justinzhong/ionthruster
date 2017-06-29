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

            // Specifying the Build tasks here
            await scope.Start<CleanProjectTask>()
                .Branch<GitVersionTask, string>()
                .Flush();
        }
    }
}
