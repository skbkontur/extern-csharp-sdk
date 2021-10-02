using System;
using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.Auth.OpenId.Builder;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Api.Client.Http.Configurations;

namespace Kontur.Extern.Api.Client.Auth.OpenId
{
    public class OpenIdAuthSetup : IAuthSetup
    {
        private readonly OpenIdAuthenticatorBuilder builder;
        private readonly Credentials creds;
        private readonly string apiKey;
        private readonly string clientId;
        private readonly string identityUrl;

        public OpenIdAuthSetup(
            OpenIdAuthenticatorBuilder builder,
            Credentials creds,
            string apiKey,
            string clientId,
            string identityUrl)
        {
            this.builder = builder;
            this.creds = creds;
            this.apiKey = apiKey;
            this.clientId = clientId;
            this.identityUrl = identityUrl;
        }

        public IConfigured Configure()
        {
            return builder.WithHttpConfiguration(new ExternalUrlHttpClientConfiguration(new Uri(identityUrl)))
                .WithClientIdentification(clientId, apiKey)
                .WithAuthenticationByPassword(creds.UserName, creds.Password);
        }
    }
}