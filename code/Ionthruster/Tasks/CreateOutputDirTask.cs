using Ionthruster.Config;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ionthruster.Tasks
{
    public class CreateOutputDirTask : ITask<int, string>
    {
        private IBuildConfig Config { get; }

        public CreateOutputDirTask(IBuildConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            Config = config;
        }

        public async Task<string> Run(int buildNumberVal)
        {
            var buildNumber = buildNumberVal.ToString();
            var path = Path.Combine(Config.OutputPath, buildNumber);

            Console.WriteLine($"[CreateOutputDirTask] Returns: {path}");

            return await Task.FromResult(path);
        }
    }
}