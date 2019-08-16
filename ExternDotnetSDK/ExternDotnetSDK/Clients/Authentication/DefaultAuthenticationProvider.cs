using Refit;

namespace ExternDotnetSDK.Clients.Authentication
{
    public class DefaultAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string login;
        private readonly string password;
        private readonly string apiKey;
        public IAuthClientRefit ClientRefit { get; }

        public DefaultAuthenticationProvider(string address, string apiKey, string password, string login)
        {
            this.apiKey = apiKey;
            this.password = password;
            this.login = login;
            ClientRefit = RestService.For<IAuthClientRefit>(address);
        }

        public string GetApiKey() => apiKey;
        public string GetSessionId() => ClientRefit.ByPass(login, password, apiKey).Result.Sid;
    }
}