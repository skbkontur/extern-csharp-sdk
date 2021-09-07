using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Http.ClusterClientAdapters;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Testing.End2End.ClusterClient
{
    public class TestingClusterClientFactory : IClusterClientFactory
    {
        private readonly string serverUrl;

        public TestingClusterClientFactory(string serverUrl) => this.serverUrl = serverUrl;

        public IClusterClient Create(ILog log) => new Vostok.Clusterclient.Core.ClusterClient(
            log,
            x =>
            {
                x.SetupUniversalTransport();
                x.Transport = new DumpRequestsAndResponsesTransport(x.Transport, x.Log);
                x.SetupExternalUrl(serverUrl.ToUrl());
                x.RepeatReplicas(1);
            }
        );
    }
}