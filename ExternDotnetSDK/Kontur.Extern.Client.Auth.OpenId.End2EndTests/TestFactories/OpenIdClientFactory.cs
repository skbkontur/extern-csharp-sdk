using Kontur.Extern.Client.Auth.OpenId.Client;
using Kontur.Extern.Client.Http.Options;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Testing.End2End.ClusterClient;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Auth.OpenId.End2EndTests.TestFactories
{
    internal static class OpenIdClientFactory
    {
        public static OpenIdClient CreateTestClient(string openIdServerUrl, ILog log)
        {
            var clusterClient = ClusterClientFactory.CreateTestClient(openIdServerUrl, log);
            return OpenIdClient.Create(new RequestTimeouts(), clusterClient, new JsonSerializer());
        }
    }
}