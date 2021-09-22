using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Api.Client.Http.Configurations;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class SpecifyAuthProviderExternBuilderExtension
    {
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private const string ClientId = "keapi.dotnetsdk";
        private static readonly Uri IdentityUrl = new("https://identity.skbkontur.ru");

        public static ExternBuilder.Configured WithPasswordAuthorization(this ExternBuilder.SpecifyAuthProvider specifyAuthProvider, Credentials credentials, string apiKey) =>
            specifyAuthProvider.WithOpenIdAuthProvider(builder => builder
                .WithHttpConfiguration(new ExternalUrlHttpClientConfiguration(IdentityUrl))
                .WithClientIdentification(ClientId, apiKey)
                .WithAuthenticationByPassword(credentials.UserName, credentials.Password)
            );
    }
}