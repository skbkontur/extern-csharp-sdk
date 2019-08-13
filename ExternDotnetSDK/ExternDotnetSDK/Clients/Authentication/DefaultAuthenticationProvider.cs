using ExternDotnetSDK.Clients.Common;
using Refit;

namespace ExternDotnetSDK.Clients.Authentication
{
    public class DefaultAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string apiKey;
        private readonly SessionResponse sessionResponse;
        public readonly IAuthClientRefit ClientRefit;

        public DefaultAuthenticationProvider(string authAddress, string login, string password, string apiKey = null)
        {
            this.apiKey = apiKey;
            ClientRefit = RestService.For<IAuthClientRefit>(authAddress);
            sessionResponse = ClientRefit.ByPass(login, password, apiKey).Result;
        }

        public string GetApiKey() => apiKey;

        public string GetSessionId() => sessionResponse.Sid;
    }
}