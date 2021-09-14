using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Auth.OpenId.Client;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses;
using Kontur.Extern.Api.Client.Auth.OpenId.Provider.Models;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Provider.AuthStrategies
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