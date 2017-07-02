namespace Ionthruster.Middleware.Build.Infrastructure
{
    public interface IPathDetector
    {
        bool DirectoryExists(string directory);
        bool FileExists(string file);
    }
}