using System;
using System.Reflection;

namespace Kontur.Extern.Api.Client.Http.VersionGetter;

public static class ClientMetaGetter
{
    public static readonly Lazy<string> ClientVersion = new(GetClientVersion);
    public static readonly Lazy<string> PackageType = new(GetPackageType);

    private static string GetClientVersion()
    {
        var version = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            .InformationalVersion;

        return version;
    }

    private static string GetPackageType()
    { 
        var attributes = Assembly
            .GetExecutingAssembly()
            .GetCustomAttributes<AssemblyMetadataAttribute>();

        foreach (var attribute in attributes)
        {
            if (attribute.Key == "IsNugetPackage" && bool.TryParse(attribute.Value, out var isNuget) && isNuget)
            {
                return "nuget";
            }
        }

        return "commit";
    }
}