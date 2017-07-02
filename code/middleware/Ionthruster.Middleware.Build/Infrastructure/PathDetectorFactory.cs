using System.IO;

namespace Ionthruster.Middleware.Build.Infrastructure
{
    internal sealed class PathDetectorFactory
    {
        public PathDetector Create()
        {
            return new PathDetector(directory => Directory.Exists(directory), file => File.Exists(file));
        }
    }
}
