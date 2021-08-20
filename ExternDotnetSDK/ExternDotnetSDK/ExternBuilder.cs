#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Auth.Abstractions;
using Kontur.Extern.Client.Auth.OpenId.Builder;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Http.Options;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Model.Configuration;
using Kontur.Extern.Client.Primitives.Polling;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public class ExternBuilder : IExternBuilder, ISpecifyAuthProviderExternBuilder
    {
        private static IPollingStrategy DefaultDelayPollingStrategy => new ConstantDelayPollingStrategy(5.Seconds());
        private static ICrypt DefaultCryptoProvider => new WinApiCrypt();
        
        public static ISpecifyAuthProviderExternBuilder WithExternApiUrl(Uri url, ILog log)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            if (!url.IsAbsoluteUri)
                throw Errors.UrlShouldBeAbsolute(nameof(url), url);

            var clusterClient = new ClusterClient(
                log,
                cfg =>
                {
                    cfg.SetupUniversalTransport();
                    cfg.SetupExternalUrl(url);
                    cfg.MaxReplicasUsedPerRequest = 1;
                });
            return new ExternBuilder(clusterClient, log);
        }
        
        public static ISpecifyAuthProviderExternBuilder WithClusterClient(IClusterClient clusterClient, ILog log) => 
            new ExternBuilder(clusterClient, log);

        private ICrypt? cryptoProvider;
        private IPollingStrategy? pollingStrategy;
        private readonly ILog log;
        private readonly IClusterClient clusterClient;
        private RequestTimeouts? requestTimeouts;
        // NOTE: nullable because here could be implementations of another auth providers 
        private OpenIdSetup? openIdAuthProviderSetup;
        private bool enableUnauthorizedFailover;
        private ContentManagementOptions? options;

        private ExternBuilder(IClusterClient clusterClient, ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
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
            var jsonSerializer = new JsonSerializer();
            var authProvider = CreateAuthProvider(jsonSerializer);

            return new ExternFactory
                {
                    EnableUnauthorizedFailover = enableUnauthorizedFailover
                }
                .Create(
                    options ?? ContentManagementOptions.Default,
                    clusterClient,
                    pollingStrategy ?? DefaultDelayPollingStrategy,
                    cryptoProvider ?? DefaultCryptoProvider,
                    requestTimeouts ?? new RequestTimeouts(),
                    authProvider,
                    jsonSerializer
                );
        }

        private IAuthenticationProvider CreateAuthProvider(IJsonSerializer jsonSerializer)
        {
            if (openIdAuthProviderSetup != null)
            {
                var builder = new OpenIdAuthenticationProviderBuilder(jsonSerializer, log);
                var configuredBuilder = openIdAuthProviderSetup(builder);
                return configuredBuilder.Build();
            }

            throw Errors.TheAuthProviderNotSpecifiedOrUnsupported();
        }
    }
}