using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.Auth.OpenId.Client;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.AuthStrategies
{
    internal class PasswordOpenIdAuthenticationStrategy : IOpenIdAuthenticationStrategy
    {
        private readonly Credentials credentials;

        public PasswordOpenIdAuthenticationStrategy(Credentials credentials) => this.credentials = credentials;

        public Task<TokenResponse> AuthenticateAsync(IOpenIdClient openId, OpenIdAuthenticationOptions options, TimeSpan? timeout)
        {
            var request = new PasswordTokenRequest(
                credentials,
                options.Scope,
                options.ClientId,
                options.ApiKey
            );
            return openId.RequestTokenAsync(request, timeout);
        }
    }
}