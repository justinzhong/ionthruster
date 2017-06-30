using NuGet;

namespace Ionthruster.Middleware.Build.Infrastructure
{
    public interface INugetPackageFinder
    {
        PackageReference FindPackage(string packagesConfig, string packageId);
    }
}