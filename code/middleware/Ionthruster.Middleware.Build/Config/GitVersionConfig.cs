using Ionthruster.Config;

namespace Ionthruster.Middleware.Build.Config
{
    public class GitVersionConfig : IConfig
    {
        public string PackagesConfig { get; set; }

        public string PackagesPath { get; set; }
    }
}