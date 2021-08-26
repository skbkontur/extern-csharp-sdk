#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Auth.Abstractions;
using Kontur.Extern.Client.Auth.OpenId.Client;
using Kontur.Extern.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Client.Auth.OpenId.Provider;
using Kontur.Extern.Client.Auth.OpenId.Provider.AuthStrategies;
using Kontur.Extern.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Client.Common.Time;
using Kontur.Extern.Client.Http.Options;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Auth.OpenId.Builder
{
    [PublicAPI]
    public class OpenIdAuthenticationProviderBuilder
    {
        public OpenIdAuthenticationProviderBuilder(ILog log) => 
            Log = log ?? throw new ArgumentNullException(nameof(log));

        public ILog Log { get; }

        public SpecifyClientIdentification WithExternApiUrl(Uri url, RequestTimeouts? requestTimeouts = null)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            if (!url.IsAbsoluteUri)
                throw Errors.UrlShouldBeAbsolute(nameof(url), url);

            var clusterClient = new ClusterClient(
                Log,
                cfg =>
                {
                    cfg.SetupUniversalTransport();
                    cfg.SetupExternalUrl(url);
                });
            return new(Log, clusterClient, requestTimeouts);
        }

        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public SpecifyClientIdentification WithClusterClient(IClusterClient clusterClient, RequestTimeouts? requestTimeouts = null) =>
            new(Log, clusterClient, requestTimeouts);

        [PublicAPI]
        public class SpecifyClientIdentification
        {
            internal SpecifyClientIdentification(ILog log, IClusterClient clusterClient, RequestTimeouts? options)
            {
                Log = log;
                ClusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
                RequestTimeouts = options;
            }

            internal RequestTimeouts? RequestTimeouts { get; }
            internal IClusterClient ClusterClient { get; }
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

            internal RequestTimeouts? RequestTimeouts => specifyClient.RequestTimeouts;
            internal IClusterClient ClusterClient => specifyClient.ClusterClient;
            internal string ApiKey { get; }
            internal string ClientId { get; }

            public Configured WithAuthenticationByPassword(string username, string password) =>
                new(new PasswordOpenIdAuthenticationStrategy(new Credentials(username, password)), this);
        }

        [PublicAPI]
        public class Configured
        {
            private readonly IOpenIdAuthenticationStrategy authenticationStrategy;
            private readonly SpecifyAuthStrategy specifyAuthStrategy;
            private TimeInterval? proactiveAuthTokenRefreshInterval;
            private IStopwatchFactory? stopwatchFactory;

            internal Configured(IOpenIdAuthenticationStrategy strategy, SpecifyAuthStrategy specifyAuthStrategy)
            {
                authenticationStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
                this.specifyAuthStrategy = specifyAuthStrategy;
            }

            [SuppressMessage("ReSharper", "ParameterHidesMember")]
            public Configured SubstituteStopwatch(IStopwatchFactory stopwatchFactory)
            {
                this.stopwatchFactory = stopwatchFactory;
                return this;
            }

            public Configured RefreshAccessTokensBeforeExpirationsProactivelyWithinInterval(TimeSpan interval)
            {
                proactiveAuthTokenRefreshInterval = interval;
                return this;
            }

            public IAuthenticationProvider Build()
            {
                stopwatchFactory ??= new SystemStopwatchFactory();
                var requestTimeouts = specifyAuthStrategy.RequestTimeouts ?? new RequestTimeouts();
                var clusterClient = specifyAuthStrategy.ClusterClient;
                var apiKey = specifyAuthStrategy.ApiKey;
                var clientId = specifyAuthStrategy.ClientId;

                var options = new OpenIdAuthenticationOptions(apiKey, clientId, proactiveAuthTokenRefreshInterval);
                var openIdClient = OpenIdClient.Create(requestTimeouts, clusterClient);
                return new OpenIdAuthenticationProvider(options, openIdClient, authenticationStrategy, stopwatchFactory);
            }
        }
    }
}