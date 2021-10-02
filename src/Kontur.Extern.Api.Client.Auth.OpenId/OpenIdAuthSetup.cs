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
        private readonly ICredentials creds;
        private readonly string apiKey;
        private readonly string clientId;
        private readonly string identityUrl;

        public OpenIdAuthSetup(
            OpenIdAuthenticatorBuilder builder,
            ICredentials creds,
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
            if (builder is not {} openIdBuilder || creds is not Credentials openIdCreds)
                throw Errors.WrongAuthSetupParameter();

            return openIdBuilder.WithHttpConfiguration(new ExternalUrlHttpClientConfiguration(new Uri(identityUrl)))
                .WithClientIdentification(clientId, apiKey)
                .WithAuthenticationByPassword(openIdCreds.UserName, openIdCreds.Password);
        }
    }
}