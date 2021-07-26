#nullable enable
using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.Authentication;
using Kontur.Extern.Client.Authentication.OpenId.Builder;
using Kontur.Extern.Client.Common;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Client.Http.Options;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives.Polling;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public class ExternFactory : IExternFactory, ISpecifyAuthProviderExternFactory
    {
        private static IPollingStrategy DefaultDelayPollingStrategy => new ConstantDelayPollingStrategy(5.Seconds());
        private static ICrypt DefaultCryptoProvider => new WinApiCrypt();
        
        public static ISpecifyAuthProviderExternFactory WithExternApiUrl(Uri url, ILog log)
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
                });
            return new ExternFactory(clusterClient, log);
        }
        
        public static ISpecifyAuthProviderExternFactory WithClusterClient(IClusterClient clusterClient, ILog log) => 
            new ExternFactory(clusterClient, log);

        private ICrypt? cryptoProvider;
        private IPollingStrategy? pollingStrategy;
        private readonly ILog log;
        private readonly IClusterClient clusterClient;
        private RequestTimeouts? requestTimeouts;
        // NOTE: nullable because here could be implementations of another auth providers 
        private OpenIdSetup? openIdAuthProviderSetup;

        private ExternFactory(IClusterClient clusterClient, ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
        }

        public ExternFactory WithCryptoProvider(ICrypt crypt)
        {
            cryptoProvider = crypt ?? throw new ArgumentNullException(nameof(crypt));
            return this;
        }
        
        public ExternFactory WithLongOperationsPollingStrategy(IPollingStrategy strategy)
        {
            pollingStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            return this;
        }

        public ExternFactory WithDefaultRequestTimeouts(TimeSpan defaultReadTimeout, TimeSpan defaultWriteTimeout, TimeSpan defaultLongOperationTimeout)
        {
            requestTimeouts = new RequestTimeouts(defaultReadTimeout, defaultWriteTimeout, defaultLongOperationTimeout);
            return this;
        }

        IExternFactory ISpecifyAuthProviderExternFactory.WithOpenIdAuthProvider(OpenIdSetup setup)
        {
            openIdAuthProviderSetup = setup ?? throw new ArgumentNullException(nameof(setup));
            return this;
        }

        public IExtern Create()
        {
            pollingStrategy ??= DefaultDelayPollingStrategy;
            cryptoProvider ??= DefaultCryptoProvider;
            requestTimeouts ??= new RequestTimeouts();
            
            var jsonSerializer = new JsonSerializer();
            var authProvider = CreateAuthProvider(jsonSerializer);

            var http = new HttpRequestsFactory(
                requestTimeouts,
                authProvider.AuthenticateRequestAsync,
                HandleApiErrors,
                clusterClient,
                jsonSerializer
            );
            var api = new KeApiClient(http, cryptoProvider);
            var services = new ExternClientServices(http, api, pollingStrategy);
            return new Extern(services);
        }

        private bool HandleApiErrors(IHttpResponse response)
        {
            var status = response.Status;
            if (status.IsClientError && response.TryGetMessage<Error>(out var errorResponse) && errorResponse.IsNotEmpty)
            {
                throw Errors.UnsuccessfulApiResponse(errorResponse);
            }

            return false;
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

        private class Extern : IExtern
        {
            private readonly IExternClientServices services;

            public Extern(IExternClientServices services) => this.services = services;

            public AccountListPath Accounts => new(services);
        }
    }
}