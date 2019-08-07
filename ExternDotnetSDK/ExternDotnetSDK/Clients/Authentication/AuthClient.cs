using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using Refit;

namespace ExternDotnetSDK.Clients.Authentication
{
    public class AuthClient : IAuthClient
    {
        public IAuthClientRefit ClientRefit { get; }

        public AuthClient(string address) => ClientRefit = RestService.For<IAuthClientRefit>(address);

        public async Task<SessionResponse> ByPass(string login, string password, string apiKey = null) =>
            await ClientRefit.ByPass(login, password, apiKey);
    }
}