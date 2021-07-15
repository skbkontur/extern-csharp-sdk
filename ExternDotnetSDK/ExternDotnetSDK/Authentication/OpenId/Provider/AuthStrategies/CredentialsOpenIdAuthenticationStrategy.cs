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
            return openId.RequestTokenAsync(
                new PasswordTokenRequest
                {
                    UserName = credentials.UserName,
                    Password = credentials.Password,
                    ClientId = options.ClientId,
                    ClientSecret = options.ApiKey,
                    Scope = options.Scope
                },
                timeout);
        }
    }
}