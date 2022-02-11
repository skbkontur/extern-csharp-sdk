using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class SpecifyAuthenticatorExternBuilderExtension
    {
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private static readonly Uri IdentityUrl = new("https://identity.skbkontur.ru");

        public static ExternBuilder.Configured WithPasswordAuthentication(this ExternBuilder.SpecifyAuthenticator specifyAuthenticator, Credentials credentials, string clientId, string apiKey) =>
            specifyAuthenticator.WithOpenIdAuthenticator(
                builder => builder
                    .WithOpenIdProviderUrl(IdentityUrl)
                    .WithClientIdentification(clientId, apiKey)
                    .WithAuthenticationByPassword(credentials.UserName, credentials.Password)
                    .Build()
            );

        public static ExternBuilder.Configured WithCertificateAuthentication(this ExternBuilder.SpecifyAuthenticator specifyAuthenticator, string certificateThumbprint, string clientId, string apiKey) =>
            specifyAuthenticator.WithOpenIdAuthenticator(
                builder => builder
                    .WithOpenIdProviderUrl(IdentityUrl)
                    .WithClientIdentification(clientId, apiKey)
                    .WithAuthenticationByCertificate(certificateThumbprint)
                    .Build()
            );

        public static ExternBuilder.Configured WithCertificateAuthentication(this ExternBuilder.SpecifyAuthenticator specifyAuthenticator, X509Certificate2 certificate, string clientId, string apiKey) =>
            specifyAuthenticator.WithOpenIdAuthenticator(
                builder => builder
                    .WithOpenIdProviderUrl(IdentityUrl)
                    .WithClientIdentification(clientId, apiKey)
                    .WithAuthenticationByCertificate(certificate)
                    .Build()
            );
    }
}