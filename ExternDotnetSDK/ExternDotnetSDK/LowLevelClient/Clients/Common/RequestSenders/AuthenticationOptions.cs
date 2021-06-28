using Kontur.Extern.Client.Clients.Authentication;

namespace Kontur.Extern.Client.Clients.Common.RequestSenders
{
    internal class AuthenticationOptions
    {
        public AuthenticationOptions(string apiKey, IAuthenticationProvider provider)
        {
            ApiKey = apiKey;
            Provider = provider;
        }

        public string ApiKey { get; }
        public IAuthenticationProvider Provider { get; }
    }
}