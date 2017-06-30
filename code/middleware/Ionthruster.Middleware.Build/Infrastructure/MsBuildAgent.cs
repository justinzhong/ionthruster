using Ionthruster.Middleware.Build.Config;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ionthruster.Middleware.Build.Infrastructure
{
    public class MsBuildAgent : IBuildAgent
    {
        private MsBuildAgentConfig Config { get; }
        private IProcessRunner ProcessRunner { get; }

        public MsBuildAgent(MsBuildAgentConfig config, IProcessRunner processRunner)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (processRunner == null) throw new ArgumentNullException(nameof(processRunner));

            Config = config;
            ProcessRunner = processRunner;
        }

        public async Task Build(string projectPath, params string[] args)
        {
            var windowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var msBuildPath = Path.GetFullPath(Path.Combine(windowsPath, Config.ToolPath));

            await ProcessRunner.Run(projectPath, msBuildPath, args);
        }
    }
}
