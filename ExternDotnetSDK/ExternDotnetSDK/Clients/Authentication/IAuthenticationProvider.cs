using System;
using System.Threading.Tasks;

namespace KeApiClientOpenSdk.Clients.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<string> GetSessionId(TimeSpan? timeout = null);
    }
}