using Ionthruster.Config;
using Ionthruster.Pipeline;
using Ionthruster.Tasks;
using System.Threading.Tasks;

namespace Ionthruster.Middleware
{
    public class BuildMiddleware : IMiddleware
    {
        public async Task Run(IPipelineScope scope)
        {
            var buildConfig = new BuildConfig();

            // Specifying the Build pipeline here
            await scope.Start<GitVersionTask>(buildConfig.ProjectPath)
                .Flush();
        }
    }
}
