using Kontur.Extern.Client.Authentication.OpenId.Client;
using Kontur.Extern.Client.End2EndTests.TestClusterClient;
using Kontur.Extern.Client.HttpLevel.ClusterClientAdapters;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Authentication.OpenId
{
    internal static class OpenIdClientFactory
    {
        public static OpenIdClient CreateTestClient(string openIdServerUrl, ILog log)
        {
            var options = new RequestSendingOptions();
            var clusterClient = ClusterClientFactory.CreateTestClient(openIdServerUrl, log);
            var http = new HttpRequestsFactory(options, clusterClient, new JsonSerializer(), log);
            return new OpenIdClient(http, log);
        }
    }
}