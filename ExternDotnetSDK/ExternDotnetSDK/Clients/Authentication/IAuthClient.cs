using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;

namespace ExternDotnetSDK.Clients.Authentication
{
    public interface IAuthClient
    {
        IAuthClientRefit ClientRefit { get; }

        Task<SessionResponse> ByPass(string login, string password, string apiKey = null);
    }
}