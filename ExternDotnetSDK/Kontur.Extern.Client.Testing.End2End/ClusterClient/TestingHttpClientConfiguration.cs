using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Http.Configurations;
using Kontur.Extern.Client.Http.Retries;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;

namespace Kontur.Extern.Client.Testing.End2End.ClusterClient
{
    public class TestingHttpClientConfiguration : IHttpClientConfiguration
    {
        private readonly string serverUrl;

        public TestingHttpClientConfiguration(string serverUrl) => this.serverUrl = serverUrl;

        public IIdempotentRequestSpecification? IdempotentRequests => null;
        public IRetryStrategyPolicy? RetryStrategy => null;

        public void Apply(IClusterClientConfiguration config)
        {
            config.Logging.LogReplicaRequests = false;
            config.Logging.LogResultDetails = false;
            config.Logging.LogRequestDetails = false;
            config.Logging.LogReplicaResults = false;
        
            config.SetupUniversalTransport();
            config.Transport = new DumpRequestsAndResponsesTransport(config.Transport, config.Log);
            config.SetupExternalUrlAsSingleReplicaCluster(serverUrl.ToUrl());
        }
    }
}