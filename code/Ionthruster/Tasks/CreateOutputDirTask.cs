using Ionthruster.Config;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ionthruster.Tasks
{
    public class CreateOutputDirTask : IFuncTask<string, string>
    {
        private ProjectConfig Config { get; }

        public CreateOutputDirTask(ProjectConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            Config = config;
        }

        public async Task<string> Run(string buildNumber)
        {
            var path = Path.Combine(Config.OutputPath, buildNumber);

            Console.WriteLine($"[CreateOutputDirTask] Returns: {path}");

            return await Task.FromResult(path);
        }
    }
}