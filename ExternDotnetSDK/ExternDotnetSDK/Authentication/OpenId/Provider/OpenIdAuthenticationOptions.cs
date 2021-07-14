using System;
using System.Diagnostics;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider
{
    internal class OpenIdAuthenticationOptions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="clientId">Client id which will be sent to the auth server. If the value is <c>null</c> then client id will be a current process name</param>
        public OpenIdAuthenticationOptions(string apiKey, string clientId = null)
        {
            ApiKey = apiKey;
            ClientId = clientId ?? Process.GetCurrentProcess().ProcessName;
        }
        
        public TimeSpan ProactiveAuthTokenRefreshInterval => TimeSpan.FromSeconds(5);
        public string ClientId { get; }
        public string ApiKey { get; }
        public string Scope => "extern.api";
    }
}