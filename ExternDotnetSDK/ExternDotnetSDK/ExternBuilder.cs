#nullable enable
using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Client.Http.Options;
using Kontur.Extern.Client.Model.Configuration;
using Kontur.Extern.Client.Primitives.Polling;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public class ExternBuilder : IExternBuilder, ISpecifyAuthProviderExternBuilder
    {
        private static IPollingStrategy DefaultDelayPollingStrategy => new ConstantDelayPollingStrategy(5.Seconds());
        private static ICrypt DefaultCryptoProvider => new WinApiCrypt();
        
        public static ISpecifyAuthProviderExternBuilder WithExternApiUrl(Uri url, ILog log) => 
            new ExternBuilder(new ExternalUrlClusterClientFactory(url), log);

        public static ISpecifyAuthProviderExternBuilder WithClusterClient(IClusterClientFactory clusterClientFactory, ILog log) => 
            new ExternBuilder(clusterClientFactory, log);

        private ICrypt? cryptoProvider;
        private IPollingStrategy? pollingStrategy;
        private readonly ILog log;
        private RequestTimeouts? requestTimeouts;
        private OpenIdSetup? openIdAuthProviderSetup;
        private bool enableUnauthorizedFailover;
        private ContentManagementOptions? contentManagementOptions;
        private IClusterClientFactory clusterClientFactory;

        private ExternBuilder(IClusterClientFactory clusterClientFactory, ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.clusterClientFactory = clusterClientFactory ?? throw new ArgumentNullException(nameof(clusterClientFactory));
        }

        public ExternBuilder WithCryptoProvider(ICrypt crypt)
        {
            cryptoProvider = crypt ?? throw new ArgumentNullException(nameof(crypt));
            return this;
        }
        
        public ExternBuilder WithLongOperationsPollingStrategy(IPollingStrategy strategy)
        {
            pollingStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            return this;
        }

        public ExternBuilder WithDefaultRequestTimeouts(TimeSpan defaultReadTimeout, TimeSpan defaultWriteTimeout, TimeSpan defaultLongOperationTimeout)
        {
            requestTimeouts = new RequestTimeouts(defaultReadTimeout, defaultWriteTimeout, defaultLongOperationTimeout);
            return this;
        }

        IExternBuilder ISpecifyAuthProviderExternBuilder.WithOpenIdAuthProvider(OpenIdSetup setup)
        {
            openIdAuthProviderSetup = setup ?? throw new ArgumentNullException(nameof(setup));
            return this;
        }

        public IExternBuilder TryResolveUnauthorizedResponsesAutomatically(bool enabled = true)
        {
            enableUnauthorizedFailover = enabled;
            return this;
        }

        public IExternBuilder OverrideContentsOptions(ContentManagementOptions options)
        {
            contentManagementOptions = options ?? throw new ArgumentNullException(nameof(options));
            return this;
        }

        public IExtern Create()
        {
            return new ExternFactory
                {
                    EnableUnauthorizedFailover = enableUnauthorizedFailover
                }
                .Create(
                    contentManagementOptions,
                    clusterClientFactory,
                    pollingStrategy,
                    cryptoProvider,
                    requestTimeouts,
                    openIdAuthProviderSetup,
                    log
                );
        }
    }
}