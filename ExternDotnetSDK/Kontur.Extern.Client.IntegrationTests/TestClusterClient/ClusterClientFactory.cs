using Kontur.Extern.Client.HttpLevel;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.IntegrationTests.TestClusterClient
{
    internal static class ClusterClientFactory
    {
        public static ClusterClient CreateTestClient(string serverUrl, ILog log) => new(log, x =>
        {
            x.SetupUniversalTransport();
            x.Transport = new DumpRequestsAndResponsesTransport(x.Transport, x.Log);
            x.SetupExternalUrl(serverUrl.ToUrl());
        });
    }
}