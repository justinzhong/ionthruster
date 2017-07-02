using System;
using System.IO;

namespace Ionthruster.Middleware.Build.Infrastructure
{
    public class PathDetector : IPathDetector
    {
        private Func<string, bool> p;

        private Func<string, bool> DirectoryDetector { get; }

        private Func<string, bool> FileDetector { get; }

        public PathDetector(Func<string, bool> directoryDetector, Func<string, bool> fileDetector)
        {
            if (directoryDetector == null) throw new ArgumentNullException(nameof(directoryDetector));

            if (fileDetector == null) throw new ArgumentNullException(nameof(fileDetector));

            DirectoryDetector = directoryDetector;
            FileDetector = fileDetector;
        }

        public PathDetector(Func<string, bool> p)
        {
            this.p = p;
        }

        public bool DirectoryExists(string directory)
        {
            if (string.IsNullOrEmpty(directory)) throw new ArgumentException("String cannot be empty or null", nameof(directory));

            return Directory.Exists(directory);
        }

        public bool FileExists(string file)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentException("String cannot be empty or null", nameof(file));

            return File.Exists(file);
        }
    }
}
