using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using Refit;

namespace ExternDotnetSDK.Clients.Authentication
{
    public class AuthClient : IAuthClient
    {
        public AuthClient(string address)
        {
            ClientRefit = RestService.For<IAuthClientRefit>(address);
        }

        public IAuthClientRefit ClientRefit { get; }

        public async Task<SessionResponse> ByPass(string login, string password, string apiKey = null)
        {
            return await ClientRefit.ByPass(login, password, apiKey);
        }
    }
}