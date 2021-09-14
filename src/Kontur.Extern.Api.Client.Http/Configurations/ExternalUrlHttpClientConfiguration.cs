using System;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Kontur.Extern.Api.Client.Http.Retries;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;

namespace Kontur.Extern.Api.Client.Http.Configurations
{
    public class ExternalUrlHttpClientConfiguration : IHttpClientConfiguration
    {
        private readonly Uri externalUrl;

        public ExternalUrlHttpClientConfiguration(Uri externalUrl)
        {
            if (externalUrl == null)
                throw new ArgumentNullException(nameof(externalUrl));
            if (!externalUrl.IsAbsoluteUri)
                throw Errors.UrlShouldBeAbsolute(nameof(externalUrl), externalUrl);
            
            this.externalUrl = externalUrl;
        }

        public IIdempotentRequestSpecification IdempotentRequests => HttpMethodBasedIdempotentRequestSpecification.SemanticallyIdempotentMethods;
        
        public IRetryStrategyPolicy RetryStrategy => new ExponentialBackOffRetryStrategyPolicy();

        public void Apply(IClusterClientConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            
            config.Logging.LogReplicaRequests = false;
            config.Logging.LogResultDetails = false;
            
            config.SetupUniversalTransport();
            config.SetupExternalUrlAsSingleReplicaCluster(externalUrl);
        }
    }
}