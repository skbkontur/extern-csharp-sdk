using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Client.Http.Configurations;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class SpecifyAuthProviderExternBuilderExtension
    {
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private const string ClientId = "keapi.dotnetsdk";
        private static readonly Uri IdentityUrl = new("https://identity.skbkontur.ru");

        public static IExternBuilder WithPasswordAuthorization(this ISpecifyAuthProviderExternBuilder externBuilder, Credentials credentials, string apiKey) =>
            externBuilder.WithOpenIdAuthProvider(builder => builder
                .WithHttpConfiguration(new ExternalUrlHttpClientConfiguration(IdentityUrl))
                .WithClientIdentification(ClientId, apiKey)
                .WithAuthenticationByPassword(credentials.UserName, credentials.Password)
            );
    }
}