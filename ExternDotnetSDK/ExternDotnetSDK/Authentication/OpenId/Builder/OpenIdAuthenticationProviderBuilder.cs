#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
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
    internal class OpenIdAuthenticationProviderBuilder : ISpecifyClusterClientOpenIdAuthenticationProviderBuilder, IOpenIdAuthenticationProviderBuilder, ISpecifyClientIdOpenIdAuthenticationProviderBuilder, ISpecifyAuthStrategyOpenIdAuthenticationProviderBuilder
    {
        private readonly IJsonSerializer serializer;
        private readonly ILog log;
        private IClusterClient clusterClient = null!;
        private string apiKey = null!;
        private string clientId = null!;
        private TimeInterval? proactiveAuthTokenRefreshInterval;
        private RequestSendingOptions? requestSendingOptions;
        private IStopwatchFactory? stopwatchFactory;
        private IOpenIdAuthenticationStrategy authenticationStrategy = null!;

        public OpenIdAuthenticationProviderBuilder(IJsonSerializer serializer, ILog log)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public ISpecifyClientIdOpenIdAuthenticationProviderBuilder WithExternApiUrl(Uri url, RequestSendingOptions? options = null)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            clusterClient = new ClusterClient(
                log,
                cfg =>
                {
                    cfg.SetupUniversalTransport();
                    cfg.SetupExternalUrl(url);
                });
            requestSendingOptions = options;
            return this;
        }
        
        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public ISpecifyClientIdOpenIdAuthenticationProviderBuilder WithClusterClient(IClusterClient clusterClient, RequestSendingOptions? options = null)
        {
            this.clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
            requestSendingOptions = options;
            return this;
        }

        IAuthenticationProvider IOpenIdAuthenticationProviderBuilder.Build()
        {
            stopwatchFactory ??= new SystemStopwatchFactory();
            requestSendingOptions ??= new RequestSendingOptions();

            var http = new HttpRequestsFactory(requestSendingOptions, clusterClient, serializer, log);
            var options = new OpenIdAuthenticationOptions(apiKey, clientId, proactiveAuthTokenRefreshInterval);
            var openIdClient = new OpenIdClient(http, log);
            return new OpenIdAuthenticationProvider(options, openIdClient, authenticationStrategy, stopwatchFactory);
        }

        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        ISpecifyAuthStrategyOpenIdAuthenticationProviderBuilder ISpecifyClientIdOpenIdAuthenticationProviderBuilder.WithClientIdentification(string clientId, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(apiKey));
            if (string.IsNullOrWhiteSpace(clientId))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(clientId));
            
            this.apiKey = apiKey;
            this.clientId = clientId;

            return this;
        }

        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        IOpenIdAuthenticationProviderBuilder IOpenIdAuthenticationProviderBuilder.SubstituteStopwatch(IStopwatchFactory stopwatchFactory)
        {
            this.stopwatchFactory = stopwatchFactory;
            return this;
        }

        IOpenIdAuthenticationProviderBuilder IOpenIdAuthenticationProviderBuilder.RefreshAccessTokensBeforeExpirationsProactivelyWithinInterval(TimeSpan interval)
        {
            proactiveAuthTokenRefreshInterval = interval;
            return this;
        }

        IOpenIdAuthenticationProviderBuilder ISpecifyAuthStrategyOpenIdAuthenticationProviderBuilder.WithAuthenticationByPassword(string username, string password)
        {
            authenticationStrategy = new PasswordOpenIdAuthenticationStrategy(new Credentials(username, password));
            return this;
        }

        public ILog Log => log;
    }
}