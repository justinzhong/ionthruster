using System;
using Ionthruster.Config;
using Ionthruster.Infrastructure;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Ionthruster.Tasks
{
    [Description(@"Cleans the build artefacts of a .NET project")]
    public class CleanProjectTask : IActionTask
    {
        private IBuildConfig Config { get; }
        private IBuildAgent BuildAgent { get; }

        public CleanProjectTask(IBuildConfig config, IBuildAgent buildAgent)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (buildAgent == null) throw new ArgumentNullException(nameof(buildAgent));

            Config = config;
            BuildAgent = buildAgent;
        }

        public async Task Run()
        {
            await BuildAgent.Build(Config.ProjectPath, "/t:Clean");
        }
    }
}