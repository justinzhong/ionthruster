using Ionthruster.Config;
using Ionthruster.Infrastructure;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ionthruster.Tasks
{
    public class GitVersionTask : ITask<string, string>
    {
        private static readonly string GitVersion = "GitVersion.CommandLine";

        private GitVersionConfig Config { get; }
        private Instrumentation.ILogger Logger { get; }
        private INugetPackageFinder PackageFinder { get; }
        private IProcessRunner ProcessRunner { get; }

        public GitVersionTask(GitVersionConfig config, Instrumentation.ILogger logger, INugetPackageFinder packageFinder, IProcessRunner processRunner)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (packageFinder == null) throw new ArgumentNullException(nameof(packageFinder));
            if (processRunner == null) throw new ArgumentNullException(nameof(processRunner));

            Config = config;
            Logger = logger;
            PackageFinder = packageFinder;
            ProcessRunner = processRunner;
        }

        public async Task<string> Run(string projectPath)
        {
            var gitVersionCliPath = GetGitVersionCliPath(projectPath);
            var gitVersionOutput = await ProcessRunner.Run(projectPath, gitVersionCliPath);
            var gitVersionInfo = JsonConvert.DeserializeObject<GitVersionInfo>(gitVersionOutput);

            await Logger.Log($"SemVer: {gitVersionInfo.FullSemVer}");

            return gitVersionInfo.FullSemVer;
        }

        private string GetGitVersionCliPath(string basePath)
        {
            var packagesConfig = Path.Combine(basePath, Config.PackagesConfig);
            var package = PackageFinder.FindPackage(packagesConfig, GitVersion);
            var packagesPath = Path.GetFullPath(Path.Combine(basePath, Config.PackagesPath));

            return Path.Combine(packagesPath, $@"GitVersion.CommandLine.{package.Version}\tools\GitVersion.exe");
        }
    }
}