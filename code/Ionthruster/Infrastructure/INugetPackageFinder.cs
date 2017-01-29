using NuGet;

namespace Ionthruster.Infrastructure
{
    public interface INugetPackageFinder
    {
        PackageReference FindPackage(string packagesConfig, string packageId);
    }
}