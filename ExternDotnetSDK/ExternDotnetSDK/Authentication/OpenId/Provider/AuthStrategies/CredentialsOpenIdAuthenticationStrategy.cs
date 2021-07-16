using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Authentication.OpenId.Client;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Authentication.OpenId.Provider.Models;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider.AuthStrategies
{
    internal class CredentialsOpenIdAuthenticationStrategy : IOpenIdAuthenticationStrategy
    {
        private readonly Credentials credentials;

        public CredentialsOpenIdAuthenticationStrategy(Credentials credentials) => this.credentials = credentials;

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