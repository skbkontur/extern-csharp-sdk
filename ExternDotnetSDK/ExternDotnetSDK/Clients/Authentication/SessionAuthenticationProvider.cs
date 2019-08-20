using System;

namespace ExternDotnetSDK.Clients.Authentication
{
    public class SessionAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string sessionId;
        private readonly string apiKey;

        public SessionAuthenticationProvider(string apiKey, string sessionId)
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