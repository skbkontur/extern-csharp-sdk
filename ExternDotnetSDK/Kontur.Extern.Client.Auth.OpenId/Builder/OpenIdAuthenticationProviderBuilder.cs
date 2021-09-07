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
using Kontur.Extern.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Client.Http.Options;
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

            var clusterClient = new ExternalUrlClusterClientFactory(url);
            return new(Log, clusterClient, requestTimeouts);
        }

        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public SpecifyClientIdentification WithClusterClient(IClusterClientFactory clusterClientFactory, RequestTimeouts? requestTimeouts = null) =>
            new(Log, clusterClientFactory, requestTimeouts);

        [PublicAPI]
        public class SpecifyClientIdentification
        {
            internal SpecifyClientIdentification(ILog log, IClusterClientFactory clusterClientFactory, RequestTimeouts? options)
            {
                Log = log;
                ClusterClientFactory = clusterClientFactory ?? throw new ArgumentNullException(nameof(clusterClientFactory));
                RequestTimeouts = options;
            }

            internal RequestTimeouts? RequestTimeouts { get; }
            internal IClusterClientFactory ClusterClientFactory { get; }
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
            internal IClusterClientFactory ClusterClientFactory => specifyClient.ClusterClientFactory;
            internal ILog Log => specifyClient.Log;
            internal string ApiKey { get; }
            internal string ClientId { get; }

            public Configured WithAuthenticationByPassword(string username, string password) =>
                new(new PasswordOpenIdAuthenticationStrategy(new Credentials(username, password)), this, Log);
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

            public Configured RefreshAccessTokensBeforeExpirationsProactivelyWithinInterval(TimeSpan interval)
            {
                proactiveAuthTokenRefreshInterval = interval;
                return this;
            }

            public IAuthenticationProvider Build()
            {
                stopwatchFactory ??= new SystemStopwatchFactory();
                var requestTimeouts = specifyAuthStrategy.RequestTimeouts ?? new RequestTimeouts();
                var clusterClientFactory = specifyAuthStrategy.ClusterClientFactory;
                var apiKey = specifyAuthStrategy.ApiKey;
                var clientId = specifyAuthStrategy.ClientId;

                var options = new OpenIdAuthenticationOptions(apiKey, clientId, proactiveAuthTokenRefreshInterval);
                var openIdClient = OpenIdClient.Create(requestTimeouts, clusterClientFactory, log);
                return new OpenIdAuthenticationProvider(options, openIdClient, authenticationStrategy, stopwatchFactory);
            }
        }
    }
}