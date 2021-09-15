using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Model.Configuration;
using Kontur.Extern.Api.Client.Primitives.Polling;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Http.Configurations;
using Kontur.Extern.Api.Client.Http.Options;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public class ExternBuilder : IExternBuilder, ISpecifyAuthProviderExternBuilder
    {
        private static IPollingStrategy DefaultDelayPollingStrategy => new ConstantDelayPollingStrategy(5.Seconds());
        private static ICrypt DefaultCryptoProvider => new WinApiCrypt();
        
        public static ISpecifyAuthProviderExternBuilder WithExternApiUrl(Uri url, ILog log) => 
            WithHttpConfiguration(new ExternalUrlHttpClientConfiguration(url), log);

        public static ISpecifyAuthProviderExternBuilder WithHttpConfiguration(IHttpClientConfiguration clientConfiguration, ILog log) => 
            new ExternBuilder(clientConfiguration, log);

        private ICrypt? cryptoProvider;
        private IPollingStrategy? pollingStrategy;
        private readonly ILog log;
        private RequestTimeouts? requestTimeouts;
        private OpenIdSetup? openIdAuthProviderSetup;
        private bool enableUnauthorizedFailover;
        private ContentManagementOptions? contentManagementOptions;
        private readonly IHttpClientConfiguration clientConfiguration;

        private ExternBuilder(IHttpClientConfiguration clientConfiguration, ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.clientConfiguration = clientConfiguration ?? throw new ArgumentNullException(nameof(clientConfiguration));
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
                    clientConfiguration,
                    pollingStrategy,
                    cryptoProvider,
                    requestTimeouts,
                    openIdAuthProviderSetup,
                    log
                );
        }
    }
}