using System;
using System.Diagnostics;
using Kontur.Extern.Client.Authentication.OpenId.Time;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider
{
    internal class OpenIdAuthenticationOptions
    {
        public readonly static TimeInterval DefaultInterval = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenIdAuthenticationOptions" /> class with the specified parameters.
        /// </summary>
        /// <param name="apiKey">ApiKey which will be send as the client secret to the auth server.</param>
        /// <param name="proactiveAuthTokenRefreshInterval">The interval before the current access token expires to refresh the current access token. By default equal to 5 seconds.</param>
        /// <param name="clientId">Client id which will be sent to the auth server. If the value is <c>null</c> then client id will be a current process name.</param>
        public OpenIdAuthenticationOptions(string apiKey, TimeInterval? proactiveAuthTokenRefreshInterval = null, string clientId = null)
        {
            ApiKey = apiKey;
            ProactiveAuthTokenRefreshInterval = proactiveAuthTokenRefreshInterval ?? DefaultInterval;
            ClientId = clientId ?? Process.GetCurrentProcess().ProcessName;
        }

        public TimeInterval ProactiveAuthTokenRefreshInterval { get; }
        public string ClientId { get; }
        public string ApiKey { get; }
        public string Scope => "extern.api";
    }
}