#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.AuthStrategies;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.Auth.OpenId.Client;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Http.Configurations;
using Kontur.Extern.Api.Client.Http.Options;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Builder
{
    [PublicAPI]
    public class OpenIdAuthenticatorBuilder
    {
        public OpenIdAuthenticatorBuilder(ICrypt cryptoProvider, ILog log)
        {
            this.cryptoProvider = cryptoProvider;
            Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public ILog Log { get; }
        private readonly ICrypt cryptoProvider;

        public SpecifyClientIdentification WithOpenIdProviderUrl(Uri url, RequestTimeouts? requestTimeouts = null)
        {
            return WithHttpConfiguration(new ExternalUrlHttpClientConfiguration(url), requestTimeouts);
        }

        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public SpecifyClientIdentification WithHttpConfiguration(IHttpClientConfiguration clientConfiguration, RequestTimeouts? requestTimeouts = null) =>
            new(clientConfiguration, cryptoProvider, requestTimeouts, Log);

        [PublicAPI]
        public class SpecifyClientIdentification
        {
            internal SpecifyClientIdentification(IHttpClientConfiguration clientConfiguration, ICrypt cryptoProvider, RequestTimeouts? options, ILog log)
            {
                ClientConfiguration = clientConfiguration ?? throw new ArgumentNullException(nameof(clientConfiguration));
                CryptoProvider = cryptoProvider;
                RequestTimeouts = options;
                Log = log;
            }

            internal IHttpClientConfiguration ClientConfiguration { get; }
            internal RequestTimeouts? RequestTimeouts { get; }
            internal readonly ICrypt CryptoProvider;
            internal ILog Log { get; set; }

            [SuppressMessage("ReSharper", "ParameterHidesMember")]
            public SpecifyAuthStrategy WithClientIdentification(string clientId, string apiKey)
            {
                return new(clientId, apiKey, this);
            }
        }

        [PublicAPI]
        public class SpecifyAuthStrategy
        {
            private readonly SpecifyClientIdentification specifyClient;

            internal SpecifyAuthStrategy(string clientId, string apiKey, SpecifyClientIdentification specifyClient)
            {
                if (string.IsNullOrWhiteSpace(apiKey))
                    throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(apiKey));
                if (string.IsNullOrWhiteSpace(clientId))
                    throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(clientId));

                this.specifyClient = specifyClient;
                ClientId = clientId;
                ApiKey = apiKey;
            }

            internal string ApiKey { get; }
            internal string ClientId { get; }
            internal RequestTimeouts? RequestTimeouts => specifyClient.RequestTimeouts;
            internal IHttpClientConfiguration ClientConfiguration => specifyClient.ClientConfiguration;
            private ICrypt CryptoProvider => specifyClient.CryptoProvider;
            private ILog Log => specifyClient.Log;

            public Configured WithAuthenticationByPassword(string username, string password) =>
                new(new PasswordOpenIdAuthenticationStrategy(new Credentials(username, password)), this, Log);

            public Configured WithAuthenticationByCertificate(string certificateThumbprint)
            {
                var certificate = CryptoProvider.GetCertificateWithPrivateKey(certificateThumbprint);
                return WithAuthenticationByCertificate(certificate);
            }

            public Configured WithAuthenticationByCertificate(X509Certificate2 certificate) =>
                new(new CertificateOpenIdAuthenticationStrategy(new CertificateCredentials {PublicKeyCertificate = certificate}, CryptoProvider), this, Log);
        }

        [PublicAPI]
        public class Configured
        {
            private readonly IOpenIdAuthenticationStrategy authenticationStrategy;
            private readonly SpecifyAuthStrategy specifyAuthStrategy;
            private TimeInterval? proactiveAuthTokenRefreshInterval;
            private IStopwatchFactory? stopwatchFactory;
            private ILog log;

            internal Configured(IOpenIdAuthenticationStrategy strategy, SpecifyAuthStrategy specifyAuthStrategy, ILog log)
            {
                authenticationStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
                this.specifyAuthStrategy = specifyAuthStrategy;
                this.log = log;
            }

            [SuppressMessage("ReSharper", "ParameterHidesMember")]
            public Configured SubstituteStopwatch(IStopwatchFactory stopwatchFactory)
            {
                this.stopwatchFactory = stopwatchFactory;
                return this;
            }

            public Configured RefreshAccessTokensBeforeExpirationProactivelyWithinInterval(TimeSpan interval)
            {
                proactiveAuthTokenRefreshInterval = interval;
                return this;
            }

            public IAuthenticator Build()
            {
                stopwatchFactory ??= new SystemStopwatchFactory();
                var requestTimeouts = specifyAuthStrategy.RequestTimeouts ?? new RequestTimeouts();
                var clientConfiguration = specifyAuthStrategy.ClientConfiguration;
                var apiKey = specifyAuthStrategy.ApiKey;
                var clientId = specifyAuthStrategy.ClientId;

                var options = new OpenIdAuthenticationOptions(apiKey, clientId, proactiveAuthTokenRefreshInterval);
                var openIdClient = OpenIdClient.Create(requestTimeouts, clientConfiguration, log);
                return new OpenIdAuthenticator(options, openIdClient, authenticationStrategy, stopwatchFactory);
            }
        }
    }
}