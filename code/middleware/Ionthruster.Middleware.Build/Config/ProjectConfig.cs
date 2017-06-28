using Ionthruster.Config;
using System;
using System.IO;

namespace Ionthruster.Middleware.Build.Config
{
    public class ProjectConfig : IConfig
    {
        private string _projectPath;

        public string OutputPath { get; set; }

        public string ProjectPath
        {
            get
            {
                return _projectPath;
            }

            set
            {
                _projectPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, value));
            }
        }
    }
}