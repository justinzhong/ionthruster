using System;
using System.Configuration;
using System.IO;

namespace Ionthruster.Config
{
    public class BuildConfig : IBuildConfig
    {
        public string OutputPath { get; }

        public string ProjectPath { get; }

        public BuildConfig()
        {
            OutputPath = ConfigurationManager.AppSettings["Build.OutputPath"];
            ProjectPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, ConfigurationManager.AppSettings["Build.ProjectPath"]));
        }
    }
}