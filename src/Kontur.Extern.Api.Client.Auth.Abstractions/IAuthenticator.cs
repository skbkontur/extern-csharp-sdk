using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Auth.Abstractions
{
    public interface IAuthenticator
    {
        Task<IAuthenticationResult> AuthenticateAsync(bool force = false, TimeSpan? timeout = null);
    }
}