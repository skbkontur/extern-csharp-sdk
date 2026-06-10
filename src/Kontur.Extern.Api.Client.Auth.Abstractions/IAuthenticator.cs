using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Auth.Abstractions
{
    [PublicAPI]
    public interface IAuthenticator
    {
        Task<IAuthenticationResult> AuthenticateAsync(bool force = false, TimeSpan? timeout = null);

        Task<UserInfo> GetCurrentSessionUserInfoAsync(TimeSpan? timeout = null);
    }
}