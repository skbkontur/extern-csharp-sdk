using System;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;

namespace ExternDotnetSDK.Clients.Common.DefaultImplementations
{
    public class DefaultAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string sessionId;
        private readonly string apiKey;

        public DefaultAuthenticationProvider(string apiKey, string sessionId)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            if (string.IsNullOrWhiteSpace(sessionId))
                throw new ArgumentNullException(nameof(sessionId));
            this.apiKey = apiKey;
            this.sessionId = sessionId;
        }

        public string GetApiKey() => apiKey;
        public string GetSessionId() => sessionId;
    }
}