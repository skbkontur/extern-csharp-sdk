using System.Net;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Http.Configurations;
using Kontur.Extern.Api.Client.Http.Retries;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;

namespace Kontur.Extern.Api.Client.Testing.End2End.ClusterClient
{
    public class TestingHttpClientConfiguration : IHttpClientConfiguration
    {
        private readonly string serverUrl;
        private readonly IWebProxy? proxy;

        public TestingHttpClientConfiguration(string serverUrl, IWebProxy? proxy = null)
        {
            this.serverUrl = serverUrl;
            this.proxy = proxy;
        }

        public IIdempotentRequestSpecification? IdempotentRequests => null;
        public IRetryStrategyPolicy? RetryStrategy => null;

        public void Apply(IClusterClientConfiguration config)
        {
            config.Logging.LogReplicaRequests = false;
            config.Logging.LogResultDetails = false;
            config.Logging.LogRequestDetails = false;
            config.Logging.LogReplicaResults = false;

            config.SetupUniversalTransport(new UniversalTransportSettings
            {
                Proxy = proxy
            });
            config.Transport = new DumpRequestsAndResponsesTransport(config.Transport, config.Log);
            config.SetupExternalUrlAsSingleReplicaCluster(serverUrl.ToUrl());
        }
    }
}