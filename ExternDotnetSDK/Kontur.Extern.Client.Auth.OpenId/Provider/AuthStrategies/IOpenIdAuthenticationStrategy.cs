using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Auth.OpenId.Client;
using Kontur.Extern.Client.Auth.OpenId.Client.Models.Responses;

namespace Kontur.Extern.Client.Auth.OpenId.Provider.AuthStrategies
{
    internal interface IOpenIdAuthenticationStrategy
    {
        Task<TokenResponse> AuthenticateAsync(IOpenIdClient openId, OpenIdAuthenticationOptions options, TimeSpan? timeout = null);
    }
}