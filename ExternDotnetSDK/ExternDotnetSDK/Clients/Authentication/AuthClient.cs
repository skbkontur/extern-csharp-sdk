using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using Refit;

namespace ExternDotnetSDK.Clients.Authentication
{
    public class AuthClient
    {
        private readonly IAuthClientRefit clientRefit;

        public AuthClient(string address)
        {
            clientRefit = RestService.For<IAuthClientRefit>(address);
        }

        public async Task<SessionResponse> ByPass(string login, string password, string apiKey = null)
        {
            return await clientRefit.ByPass(login, password, apiKey);
        }
    }
}