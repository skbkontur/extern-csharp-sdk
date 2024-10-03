using System;
using System.Collections.Generic;
using System.Net;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Http.Configurations;
using Kontur.Extern.Api.Client.Http.Retries;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Transport;

namespace Kontur.Extern.Api.Client.Testing.End2End.ClusterClient
{
    public class TestingHttpClientConfiguration : IHttpClientConfiguration
    {
        private readonly string serverUrl;

        public TestingHttpClientConfiguration(string serverUrl) => this.serverUrl = serverUrl;

        public IIdempotentRequestSpecification? IdempotentRequests => null;
        public IRetryStrategyPolicy? RetryStrategy => null;
        private readonly List<Func<Request, Request>> requestTransforms = new();
        private IWebProxy? webProxy;

        [UsedImplicitly]
        public TestingHttpClientConfiguration WithRequestTransform(Func<Request, Request> requestTransform)
        {
            requestTransforms.Add(requestTransform);

            return this;
        }
        [UsedImplicitly]
        public TestingHttpClientConfiguration WithWebProxy(IWebProxy proxy)
        {
            webProxy = proxy;
            return this;
        }
        public void Apply(IClusterClientConfiguration config)
        {
            config.Logging.LogReplicaRequests = false;
            config.Logging.LogResultDetails = false;
            config.Logging.LogRequestDetails = false;
            config.Logging.LogReplicaResults = false;

            foreach (var requestTransform in requestTransforms)
                config.AddRequestTransform(requestTransform);

            if (webProxy != null)
                config.SetupUniversalTransport(new UniversalTransportSettings() { Proxy = webProxy });
            else
                config.SetupUniversalTransport();

            config.SetupExternalUrlAsSingleReplicaCluster(serverUrl.ToUrl());
        }
    }
}