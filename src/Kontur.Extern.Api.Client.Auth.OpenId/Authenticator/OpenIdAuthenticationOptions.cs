using System;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator
{
    internal class OpenIdAuthenticationOptions
    {
        public static readonly TimeInterval DefaultInterval = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenIdAuthenticationOptions" /> class with the specified parameters.
        /// </summary>
        /// <param name="apiKey">ApiKey which will be send as the client secret to the auth server.</param>
        /// <param name="clientId">Client id which will be sent to the auth server.</param>
        /// <param name="useRefreshTokens"></param>
        /// <param name="allowUserInfoRequest"></param>
        /// <param name="proactiveAuthTokenRefreshInterval">The interval before the current access token expires to refresh the current access token. By default equal to 5 seconds.</param>
        public OpenIdAuthenticationOptions(
            string apiKey,
            string clientId,
            bool useRefreshTokens,
            bool allowUserInfoRequest,
            TimeInterval? proactiveAuthTokenRefreshInterval = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(apiKey));

            if (string.IsNullOrWhiteSpace(clientId))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(clientId));

            ApiKey = apiKey;
            ProactiveAuthTokenRefreshInterval = proactiveAuthTokenRefreshInterval ?? DefaultInterval;
            ClientId = clientId;
            UseRefreshTokens = useRefreshTokens;
            AllowUserInfoRequest = allowUserInfoRequest;
        }

        public TimeInterval ProactiveAuthTokenRefreshInterval { get; }
        public string ClientId { get; }
        public string ApiKey { get; }
        public bool UseRefreshTokens { get; }
        public bool AllowUserInfoRequest { get; }
        public string Scope => "extern.api"
                               + (UseRefreshTokens ? " offline_access" : "")
                               + (AllowUserInfoRequest ? " openid profile email" : "");
    }
}