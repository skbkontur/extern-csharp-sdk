using System.Reflection;

namespace Kontur.Extern.Api.Client.Http.VersionGetter;

public static class ClientVersionGetter 
{
    public static string GetClientVersion()
    {
        var version = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            .InformationalVersion;
        
        return version;
    }
}