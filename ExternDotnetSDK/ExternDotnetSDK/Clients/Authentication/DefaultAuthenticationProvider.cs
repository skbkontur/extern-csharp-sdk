using Refit;

namespace ExternDotnetSDK.Clients.Authentication
{
    public class DefaultAuthenticationProvider : IAuthenticationProvider
    {
        public readonly IAuthClientRefit ClientRefit;
        private readonly string login;
        private readonly string password;
        private readonly string apiKey;

        public DefaultAuthenticationProvider(string authAddress, string login, string password, string apiKey = null)
        {
            this.login = login;
            this.password = password;
            this.apiKey = apiKey;
            ClientRefit = RestService.For<IAuthClientRefit>(authAddress);
        }

        public string GetApiKey() => apiKey;

        public string GetSessionId() => ClientRefit.ByPass(login, password, apiKey).Result.Sid;
    }
}