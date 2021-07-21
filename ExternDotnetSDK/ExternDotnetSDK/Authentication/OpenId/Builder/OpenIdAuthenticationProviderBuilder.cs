#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Authentication.OpenId.Client;
using Kontur.Extern.Client.Authentication.OpenId.Provider;
using Kontur.Extern.Client.Authentication.OpenId.Provider.AuthStrategies;
using Kontur.Extern.Client.Authentication.OpenId.Provider.Models;
using Kontur.Extern.Client.Authentication.OpenId.Time;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.HttpLevel.ClusterClientAdapters;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Authentication.OpenId.Builder
{
    [PublicAPI]
    public class OpenIdAuthenticationProviderBuilder
    {
        private readonly IJsonSerializer serializer;

        public OpenIdAuthenticationProviderBuilder(IJsonSerializer serializer, ILog log)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public ILog Log { get; }

        public SpecifyClientIdentification WithExternApiUrl(Uri url, RequestSendingOptions? options = null)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            var clusterClient = new ClusterClient(
                Log,
                cfg =>
                {
                    cfg.SetupUniversalTransport();
                    cfg.SetupExternalUrl(url);
                });
            return new(serializer, Log, clusterClient, options);
        }

        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public SpecifyClientIdentification WithClusterClient(IClusterClient clusterClient, RequestSendingOptions? options = null) =>
            new(serializer, Log, clusterClient, options);

        [PublicAPI]
        public class SpecifyClientIdentification
        {
            internal SpecifyClientIdentification(IJsonSerializer serializer, ILog log, IClusterClient clusterClient, RequestSendingOptions? options)
            {
                Serializer = serializer;
                Log = log;
                ClusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
                RequestSendingOptions = options;
            }

            internal RequestSendingOptions? RequestSendingOptions { get; }
            internal IClusterClient ClusterClient { get; }
            internal IJsonSerializer Serializer { get; }
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

            internal RequestSendingOptions? RequestSendingOptions => specifyClient.RequestSendingOptions;
            internal IClusterClient ClusterClient => specifyClient.ClusterClient;
            internal IJsonSerializer Serializer => specifyClient.Serializer;
            internal ILog Log => specifyClient.Log;
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
                var requestSendingOptions = specifyAuthStrategy.RequestSendingOptions ?? new RequestSendingOptions();
                var log = specifyAuthStrategy.Log;
                var clusterClient = specifyAuthStrategy.ClusterClient;
                var serializer = specifyAuthStrategy.Serializer;
                var apiKey = specifyAuthStrategy.ApiKey;
                var clientId = specifyAuthStrategy.ClientId;

                var http = new HttpRequestsFactory(requestSendingOptions, clusterClient, serializer, log);
                var options = new OpenIdAuthenticationOptions(apiKey, clientId, proactiveAuthTokenRefreshInterval);
                var openIdClient = new OpenIdClient(http, log);
                return new OpenIdAuthenticationProvider(options, openIdClient, authenticationStrategy, stopwatchFactory);
            }
        }
    }
}