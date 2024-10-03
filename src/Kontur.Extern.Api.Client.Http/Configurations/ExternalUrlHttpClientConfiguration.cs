using System;
using System.Collections.Generic;
using System.Net;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Kontur.Extern.Api.Client.Http.Retries;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
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

        private readonly List<Func<Request, Request>> requestTransforms = new();
        private IWebProxy? webProxy;

        [UsedImplicitly]
        public ExternalUrlHttpClientConfiguration WithRequestTransform(Func<Request, Request> requestTransform)
        {
            requestTransforms.Add(requestTransform);

            return this;
        }

        [UsedImplicitly]
        public ExternalUrlHttpClientConfiguration WithWebProxy(IWebProxy proxy)
        {
            webProxy = proxy;
            return this;
        }

        public void Apply(IClusterClientConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.Logging.LogReplicaRequests = false;
            config.Logging.LogResultDetails = false;

            foreach (var requestTransform in requestTransforms)
                config.AddRequestTransform(requestTransform);

            if (webProxy != null)
                config.SetupUniversalTransport(new UniversalTransportSettings {Proxy = webProxy});
            else
                config.SetupUniversalTransport();

            config.SetupExternalUrlAsSingleReplicaCluster(externalUrl);
        }
    }
}