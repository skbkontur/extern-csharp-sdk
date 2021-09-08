using System;
using Kontur.Extern.Client.Http.Configurations;
using Kontur.Extern.Client.Http.Retries;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Retry;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Http.ClusterClientAdapters
{
    internal static class ClusterClientFactory
    {
        public static IClusterClient CreateClusterClient(IHttpClientConfiguration configuration, ILog log)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            return new ClusterClient(
                log,
                config =>
                {
                    config.Logging.LogReplicaRequests = false;
                    config.Logging.LogResultDetails = false;

                    configuration.Apply(config);
                    
                    if (configuration.RetryStrategy != null)
                    {
                        var idempotentRequests = configuration.IdempotentRequests ??
                                                 HttpMethodBasedIdempotentRequestSpecification.OnlyGetMethodIsIdempotent;
                        config.RetryPolicy = new AdHocRetryPolicy((request, _, _) => idempotentRequests.IsIdempotent(request));
                        config.RetryStrategyEx = new RetryStrategyExAdapter(configuration.RetryStrategy.CreateRetryStrategy());
                    }
                });
        }
    }
}