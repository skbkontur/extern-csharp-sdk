using System;

namespace ExternDotnetSDK.Clients.Common.AuthenticationProviders
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly string sessionId;

        public AuthenticationProvider(string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
                throw new ArgumentNullException(nameof(sessionId));
            this.sessionId = sessionId;
        }

        public string GetSessionId() => sessionId;
    }
}