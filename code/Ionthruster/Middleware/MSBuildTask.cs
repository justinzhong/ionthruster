using Ionthruster.Config;
using Ionthruster.Instrumentation;
using Ionthruster.Tasks;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Middleware
{
    public class MSBuildTask : ITask<string, string>
    {
        private IBuildConfig Config { get; }
        private ILogger Logger { get; }

        public MSBuildTask(IBuildConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            Config = config;
        }

        public async Task<string> Run(string outputPath)
        {
            await Logger.Log($"OutputPath: {outputPath}");
            await Task.Run(() => { });

            return outputPath;
        }
    }
}