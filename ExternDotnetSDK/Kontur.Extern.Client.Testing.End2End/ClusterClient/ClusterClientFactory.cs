using Kontur.Extern.Client.Http;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Testing.End2End.ClusterClient
{
    public static class ClusterClientFactory
    {
        public static Vostok.Clusterclient.Core.ClusterClient CreateTestClient(string serverUrl, ILog log) => new(log, x =>
        {
            x.SetupUniversalTransport();
            x.Transport = new DumpRequestsAndResponsesTransport(x.Transport, x.Log);
            x.SetupExternalUrl(serverUrl.ToUrl());
        });
    }
}