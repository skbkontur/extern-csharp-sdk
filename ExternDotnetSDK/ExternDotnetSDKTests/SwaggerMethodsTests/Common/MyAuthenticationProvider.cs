using ExternDotnetSDK.Clients.Common;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Common
{
    public class MyAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string login;
        private readonly string password;
        private readonly string apiKey;

        public MyAuthenticationProvider(string address, string apiKey, string password, string login)
        {
            this.apiKey = apiKey;
            this.password = password;
            this.login = login;
            ClientRefit = RestService.For<IAuthClientRefit>(address);
        }

        public IAuthClientRefit ClientRefit { get; }

        public string GetApiKey() => apiKey;
        public string GetSessionId() => ClientRefit.ByPass(login, password, apiKey).Result.Sid;
    }
}