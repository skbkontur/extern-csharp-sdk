using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<IAuthenticationResult> AuthenticateAsync(TimeSpan? timeout = null);
    }
}