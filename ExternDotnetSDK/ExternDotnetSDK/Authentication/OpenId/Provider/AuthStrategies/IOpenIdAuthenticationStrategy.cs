using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Authentication.OpenId.Client;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider.AuthStrategies
{
    internal interface IOpenIdAuthenticationStrategy
    {
        Task<TokenResponse> AuthenticateAsync(IOpenIdClient openId, OpenIdAuthenticationOptions options, TimeSpan? timeout = null);
    }
}