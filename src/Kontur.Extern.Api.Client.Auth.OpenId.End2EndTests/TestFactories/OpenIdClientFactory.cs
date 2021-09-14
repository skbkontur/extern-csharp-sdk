using Kontur.Extern.Api.Client.Auth.OpenId.Client;
using Kontur.Extern.Api.Client.Http.Options;
using Kontur.Extern.Api.Client.Testing.End2End.ClusterClient;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.Auth.OpenId.End2EndTests.TestFactories
{
    internal static class OpenIdClientFactory
    {
        public static OpenIdClient CreateTestClient(string openIdServerUrl, ILog log) => 
            OpenIdClient.Create(new RequestTimeouts(), new TestingHttpClientConfiguration(openIdServerUrl), log);
    }
}