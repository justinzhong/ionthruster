using Ionthruster.Build.Config;
using Ionthruster.Build.Infrastructure;
using Ionthruster.Tasks;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Ionthruster.Build.Tasks
{
    [Description(@"Cleans the build artefacts of a .NET project")]
    public class CleanProjectTask : IActionTask
    {
        private ProjectConfig Config { get; }
        private IBuildAgent BuildAgent { get; }

        public CleanProjectTask(ProjectConfig config, IBuildAgent buildAgent)
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