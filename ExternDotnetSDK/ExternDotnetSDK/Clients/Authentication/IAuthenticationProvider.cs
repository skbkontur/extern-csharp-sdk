using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Clients.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<string> GetSessionId(TimeSpan? timeout = null);
    }
}