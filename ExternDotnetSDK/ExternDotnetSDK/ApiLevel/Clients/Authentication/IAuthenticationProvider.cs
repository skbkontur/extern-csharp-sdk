using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<string> GetSessionId(TimeSpan? timeout = null);
    }
}