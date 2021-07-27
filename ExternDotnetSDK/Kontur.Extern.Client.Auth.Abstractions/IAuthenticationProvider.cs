using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Auth.Abstractions
{
    public interface IAuthenticationProvider
    {
        Task<IAuthenticationResult> AuthenticateAsync(bool force = false, TimeSpan? timeout = null);
    }
}