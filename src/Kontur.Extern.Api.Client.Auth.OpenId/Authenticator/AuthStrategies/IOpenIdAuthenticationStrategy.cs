using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Auth.OpenId.Client;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.AuthStrategies
{
    internal interface IOpenIdAuthenticationStrategy
    {
        Task<TokenResponse> AuthenticateAsync(IOpenIdClient openId, OpenIdAuthenticationOptions options, TimeSpan? timeout = null);
    }
}