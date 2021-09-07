#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Json;
using Kontur.Extern.Client.Auth.Abstractions;
using Kontur.Extern.Client.Auth.OpenId.Builder;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Exceptions;
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
        // NOTE: nullable because here could be implementations of another auth providers 
        private OpenIdSetup? openIdAuthProviderSetup;
        private bool enableUnauthorizedFailover;
        private ContentManagementOptions? options;
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

        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public IExternBuilder OverrideContentsOptions(ContentManagementOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            return this;
        }

        public IExtern Create()
        {
            var apiJsonSerializer = JsonSerializerFactory.CreateJsonSerializer();
            var authProvider = CreateAuthProvider();

            return new ExternFactory
                {
                    EnableUnauthorizedFailover = enableUnauthorizedFailover
                }
                .Create(
                    options ?? ContentManagementOptions.Default,
                    clusterClientFactory,
                    pollingStrategy ?? DefaultDelayPollingStrategy,
                    cryptoProvider ?? DefaultCryptoProvider,
                    requestTimeouts ?? new RequestTimeouts(),
                    authProvider,
                    apiJsonSerializer,
                    log
                );
        }

        private IAuthenticationProvider CreateAuthProvider()
        {
            if (openIdAuthProviderSetup != null)
            {
                var builder = new OpenIdAuthenticationProviderBuilder(log);
                var configuredBuilder = openIdAuthProviderSetup(builder);
                return configuredBuilder.Build();
            }

            throw Errors.TheAuthProviderNotSpecifiedOrUnsupported();
        }
    }
}