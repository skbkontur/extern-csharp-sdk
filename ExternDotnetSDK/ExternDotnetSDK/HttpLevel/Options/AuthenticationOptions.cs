using Kontur.Extern.Client.ApiLevel.Clients.Authentication;

namespace Kontur.Extern.Client.HttpLevel.Options
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