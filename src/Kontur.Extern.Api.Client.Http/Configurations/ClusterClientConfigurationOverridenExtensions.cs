using System;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Ordering;
using Vostok.Clusterclient.Core.Strategies;
using Vostok.Clusterclient.Core.Topology;

namespace Kontur.Extern.Api.Client.Http.Configurations
{
    public static class ClusterClientConfigurationOverridenExtensions
    {
        public static void SetupExternalUrlAsSingleReplicaCluster(this IClusterClientConfiguration configuration, Uri url)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            if (!url.IsAbsoluteUri)
                throw Errors.UrlShouldBeAbsolute(nameof(url), url);

            configuration.ClusterProvider = new FixedClusterProvider(url);
            configuration.MaxReplicasUsedPerRequest = 1;
            configuration.ReplicaOrdering = new AsIsReplicaOrdering();
            configuration.TargetServiceName = url.AbsoluteUri;
            configuration.SetupReplicaBudgeting(minimumRequests: 10);

            configuration.DefaultRequestStrategy = Strategy.Sequential1;
            configuration.DeduplicateRequestUrl = true;
        }
    }
}