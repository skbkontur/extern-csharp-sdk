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
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Authentication.OpenId.Builder
{
    internal class OpenIdAuthenticationProviderBuilder : IOpenIdAuthenticationProviderBuilder, ISpecifyClientIdOpenIdAuthenticationProviderBuilder, ISpecifyAuthStrategyOpenIdAuthenticationProviderBuilder
    {
        private readonly ILog log;
        private string apiKey = null!;
        private string clientId = null!;
        private TimeInterval? proactiveAuthTokenRefreshInterval;
        private IStopwatchFactory? stopwatchFactory;
        private IOpenIdAuthenticationStrategy authenticationStrategy = null!;
        private readonly HttpRequestsFactory http;

        public OpenIdAuthenticationProviderBuilder(RequestSendingOptions requestSendingOptions, IClusterClient clusterClient, IJsonSerializer serializer, ILog log)
        {
            this.log = log;
            http = new HttpRequestsFactory(requestSendingOptions, clusterClient, serializer, log);
        }

        IAuthenticationProvider IOpenIdAuthenticationProviderBuilder.Build()
        {
            stopwatchFactory ??= new SystemStopwatchFactory();

            var options = new OpenIdAuthenticationOptions(apiKey, clientId, proactiveAuthTokenRefreshInterval);
            var openIdClient = new OpenIdClient(http, log);
            return new OpenIdAuthenticationProvider(options, openIdClient, authenticationStrategy, stopwatchFactory);
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

        IOpenIdAuthenticationProviderBuilder ISpecifyAuthStrategyOpenIdAuthenticationProviderBuilder.WithAuthenticationByPassword(string username, string password)
        {
            authenticationStrategy = new PasswordOpenIdAuthenticationStrategy(new Credentials(username, password));
            return this;
        }
    }
}