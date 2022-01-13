using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel;
using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Auth.OpenId.Builder;
using Kontur.Extern.Api.Client.Model.Configuration;
using Kontur.Extern.Api.Client.Primitives.Polling;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Http.Configurations;
using Kontur.Extern.Api.Client.Http.Options;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public class ExternBuilder
    {
        private static IPollingStrategy DefaultDelayPollingStrategy => new ConstantDelayPollingStrategy(5.Seconds());
        private static ICrypt DefaultCryptoProvider => new WinApiCrypt();

        public SpecifyAuthenticator WithExternApiUrl(Uri url, ILog log) => 
            WithHttpConfiguration(new ExternalUrlHttpClientConfiguration(url), log);

        public SpecifyAuthenticator WithHttpConfiguration(IHttpClientConfiguration clientConfiguration, ILog log) => 
            new(clientConfiguration, log);

        [PublicAPI]
        public class SpecifyAuthenticator
        {
            private readonly ILog log;
            private readonly IHttpClientConfiguration clientConfiguration;

            internal SpecifyAuthenticator(IHttpClientConfiguration clientConfiguration, ILog log)
            {
                this.log = log ?? throw new ArgumentNullException(nameof(log));
                this.clientConfiguration = clientConfiguration ?? throw new ArgumentNullException(nameof(clientConfiguration));
            }

            public Configured WithAuthenticator(IAuthenticator authenticator) =>
                new(clientConfiguration, log, authenticator ?? throw new ArgumentNullException(nameof(authenticator)));

            public Configured WithOpenIdAuthenticator(OpenIdSetup setup) =>
                new(clientConfiguration, log, setup ?? throw new ArgumentNullException(nameof(setup)));
        }

        [PublicAPI]
        public class Configured
        {
            private ICrypt? cryptoProvider;
            private IPollingStrategy? pollingStrategy;
            private readonly ILog log;
            private RequestTimeouts? requestTimeouts;
            private readonly IAuthenticator authenticator;
            private bool enableUnauthorizedFailover;
            private ContentManagementOptions? contentManagementOptions;
            private readonly IHttpClientConfiguration clientConfiguration;
            private IExternHttpClient? apiClient;
            private IHttpRequestFactory? httpRequestFactory;

            internal Configured(IHttpClientConfiguration clientConfiguration, ILog log, IAuthenticator authenticator)
            {
                this.log = log ?? throw new ArgumentNullException(nameof(log));
                this.authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
                this.clientConfiguration = clientConfiguration ?? throw new ArgumentNullException(nameof(clientConfiguration));
            }

            internal Configured(IHttpClientConfiguration clientConfiguration, ILog log, OpenIdSetup openIdAuthProviderSetup)
            {
                this.log = log ?? throw new ArgumentNullException(nameof(log));
                authenticator = CreateAuthenticator(openIdAuthProviderSetup, log);
                this.clientConfiguration = clientConfiguration ?? throw new ArgumentNullException(nameof(clientConfiguration));
            }

            public Configured WithCryptoProvider(ICrypt crypt)
            {
                cryptoProvider = crypt ?? throw new ArgumentNullException(nameof(crypt));
                return this;
            }

            public Configured WithLongOperationsPollingStrategy(IPollingStrategy strategy)
            {
                pollingStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
                return this;
            }

            public Configured WithDefaultRequestTimeouts(TimeSpan defaultReadTimeout, TimeSpan defaultWriteTimeout, TimeSpan defaultLongOperationTimeout)
            {
                requestTimeouts = new RequestTimeouts(defaultReadTimeout, defaultWriteTimeout, defaultLongOperationTimeout);
                return this;
            }

            public Configured WithExternHttpClient(IExternHttpClient client)
            {
                apiClient = client;
                return this;
            }

            public Configured WithHttpRequestFactory(IHttpRequestFactory factory)
            {
                httpRequestFactory = factory;
                return this;
            }

            public Configured TryResolveUnauthorizedResponsesAutomatically(bool enabled = true)
            {
                enableUnauthorizedFailover = enabled;
                return this;
            }

            public Configured OverrideContentsOptions(ContentManagementOptions options)
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
                        authenticator,
                        apiClient,
                        httpRequestFactory,
                        log
                    );
            }

            private static IAuthenticator CreateAuthenticator(OpenIdSetup? openIdSetup, ILog log)
            {
                if (openIdSetup is not null)
                {
                    var builder = new OpenIdAuthenticatorBuilder(log);
                    var configuredBuilder = openIdSetup(builder);
                    return configuredBuilder.Build();
                }

                throw Errors.TheAuthenticatorNotSpecifiedOrUnsupported();
            }
        }
    }
}