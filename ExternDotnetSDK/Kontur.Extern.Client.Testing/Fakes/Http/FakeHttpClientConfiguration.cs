using System;
using Kontur.Extern.Client.Http.Configurations;
using Kontur.Extern.Client.Http.Retries;
using Vostok.Clusterclient.Core;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeHttpClientConfiguration : IHttpClientConfiguration
    {
        private readonly string baseUrl;
        private readonly FakeHttpMessages httpMessages;

        public FakeHttpClientConfiguration(string baseUrl, FakeHttpMessages httpMessages, FakeRetryStrategyPolicy retryStrategyPolicy)
        {
            this.baseUrl = baseUrl;
            this.httpMessages = httpMessages;
            RetryStrategy = retryStrategyPolicy;
            IdempotentRequests = retryStrategyPolicy.IdempotentRequests;
        }

        public IIdempotentRequestSpecification? IdempotentRequests { get; }
        public IRetryStrategyPolicy RetryStrategy { get; }

        public void Apply(IClusterClientConfiguration config)
        {
            config.SetupExternalUrlAsSingleReplicaCluster(new Uri(baseUrl));
            config.Transport = new FakeTransport(httpMessages);
        }
    }
}