using System;
using System.Net;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Kontur.Extern.Api.Client.Http.Retries;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;

namespace Kontur.Extern.Api.Client.Http.Configurations
{
    public class ExternalUrlHttpClientConfiguration : IHttpClientConfiguration
    {
        private readonly Uri externalUrl;
        private readonly IWebProxy? proxy;

        public ExternalUrlHttpClientConfiguration(Uri externalUrl, IWebProxy? proxy = null)
        {
            if (externalUrl == null)
                throw new ArgumentNullException(nameof(externalUrl));
            if (!externalUrl.IsAbsoluteUri)
                throw Errors.UrlShouldBeAbsolute(nameof(externalUrl), externalUrl);

            this.externalUrl = externalUrl;
            this.proxy = proxy;
        }

        public IIdempotentRequestSpecification IdempotentRequests => HttpMethodBasedIdempotentRequestSpecification.SemanticallyIdempotentMethods;

        public IRetryStrategyPolicy RetryStrategy => new ExponentialBackOffRetryStrategyPolicy();

        public void Apply(IClusterClientConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.Logging.LogReplicaRequests = false;
            config.Logging.LogResultDetails = false;

            config.SetupUniversalTransport(new UniversalTransportSettings
            {
                Proxy = proxy
            });
            config.SetupExternalUrlAsSingleReplicaCluster(externalUrl);
        }
    }
}