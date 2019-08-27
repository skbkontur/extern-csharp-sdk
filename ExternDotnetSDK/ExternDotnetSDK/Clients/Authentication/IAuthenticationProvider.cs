using System;
using System.Threading.Tasks;

namespace KeApiOpenSdk.Clients.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<string> GetSessionId(TimeSpan? timeout = null);
    }
}