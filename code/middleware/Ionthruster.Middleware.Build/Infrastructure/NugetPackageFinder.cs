using NuGet;
using System;
using System.Linq;

namespace Ionthruster.Middleware.Build.Infrastructure
{
    public class NugetPackageFinder : INugetPackageFinder
    {
        public PackageReference FindPackage(string packagesConfig, string packageId)
        {
            if (string.IsNullOrEmpty(packagesConfig)) throw new ArgumentException("String cannot be empty or null", nameof(packagesConfig));
            if (string.IsNullOrEmpty(packageId)) throw new ArgumentException("String cannot be empty or null", nameof(packageId));

            var packagesReference = new PackageReferenceFile(packagesConfig);
            var package = packagesReference.GetPackageReferences().SingleOrDefault(p => string.Equals(p.Id, packageId, StringComparison.OrdinalIgnoreCase));

            if (package == null) throw new ApplicationException($"The Nuget package {packageId} was not found in the configuration file specified: {packagesConfig}");

            return package;
        }
    }
}
