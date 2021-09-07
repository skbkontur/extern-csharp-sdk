using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Http.Configurations;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;

namespace Kontur.Extern.Client.Testing.End2End.ClusterClient
{
    public class TestingHttpClientConfiguration : IHttpClientConfiguration
    {
        private readonly string serverUrl;

        public TestingHttpClientConfiguration(string serverUrl) => this.serverUrl = serverUrl;
        
        public void Apply(IClusterClientConfiguration config)
        {
            config.Logging.LogReplicaRequests = false;
            config.Logging.LogResultDetails = false;
            config.Logging.LogRequestDetails = false;
            config.Logging.LogReplicaResults = false;
        
            config.SetupUniversalTransport();
            config.Transport = new DumpRequestsAndResponsesTransport(config.Transport, config.Log);
            config.SetupExternalUrl(serverUrl.ToUrl());
            config.RepeatReplicas(1);
        }
    }
}