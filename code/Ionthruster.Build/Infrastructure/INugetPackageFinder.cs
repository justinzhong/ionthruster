using NuGet;

namespace Ionthruster.Build.Infrastructure
{
    public interface INugetPackageFinder
    {
        PackageReference FindPackage(string packagesConfig, string packageId);
    }
}