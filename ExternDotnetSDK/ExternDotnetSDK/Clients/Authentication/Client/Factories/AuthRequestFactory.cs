using System;
using System.Linq;
using System.Net.Http;
using Kontur.Extern.Client.Clients.Authentication.Client.Extensions;
using Kontur.Extern.Client.Clients.Authentication.Client.Models;
using Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Factories
{
    internal static class AuthRequestFactory
    {
        public static HttpRequestMessage RefreshTokenRequest(RefreshTokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId))
                throw new ArgumentNullException(nameof(request.ClientId));

            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                throw new ArgumentNullException(nameof(request.RefreshToken));

            var content = new FormUrlEncodedContentBuilder()
                .AddGrantType(ClientConstants.GrantTypes.RefreshToken)
                .Add(ClientConstants.RefreshTokenRequest.RefreshToken, request.RefreshToken)
                .AddScope(request.Scope)
                .ToString();

            return BuildOpenIdClientAuthenticatedRequest(content, request);
        }

        public static HttpRequestMessage PasswordTokenRequest(PasswordTokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId))
                throw new ArgumentNullException(nameof(request.ClientId));

            if (string.IsNullOrWhiteSpace(request.UserName))
                throw new ArgumentNullException(nameof(request.UserName));

            var content = new FormUrlEncodedContentBuilder()
                .AddGrantType(ClientConstants.GrantTypes.Password)
                .AddScope(request.Scope)
                .Add(ClientConstants.PasswordTokenRequest.UserName, request.UserName)
                .Add(ClientConstants.PasswordTokenRequest.Password, request.Password)
                .Add(ClientConstants.PasswordTokenRequest.PartialFactorToken, request.PartialFactorToken)
                .ToString();

            return BuildOpenIdClientAuthenticatedRequest(content, request);
        }

        public static HttpRequestMessage CertificateTokenRequest(CertificateTokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId))
                throw new ArgumentNullException(nameof(request.ClientId));

            if (string.IsNullOrWhiteSpace(request.Thumbprint))
                throw new ArgumentNullException(nameof(request.Thumbprint));

            if (request.DecryptedKey == null)
                throw new ArgumentNullException(nameof(request.DecryptedKey));

            var content = new FormUrlEncodedContentBuilder()
                .AddGrantType(ClientConstants.GrantTypes.Certificate)
                .AddScope(request.Scope)
                .Add(ClientConstants.CertificateTokenRequest.DecryptedKey, Convert.ToBase64String(request.DecryptedKey))
                .Add(ClientConstants.CertificateTokenRequest.Thumbprint, request.Thumbprint)
                .ToString();

            return BuildOpenIdClientAuthenticatedRequest(content, request);
        }

        public static HttpRequestMessage CertificateAuthenticationRequest(CertificateAuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId))
                throw new ArgumentNullException(nameof(request.ClientId));

            if (request.PublicKey == null)
                throw new ArgumentNullException(nameof(request.PublicKey));

            var content = new FormUrlEncodedContentBuilder()
                .Add(
                    ClientConstants.CertificateAuthenticationRequest.PublicKey,
                    Convert.ToBase64String(request.PublicKey.GetRawCertData()))
                .Add(ClientConstants.CertificateAuthenticationRequest.IsFree, request.Free.ToString())
                .AddIfNotNull(ClientConstants.CertificateAuthenticationRequest.PartialFactorToken, request.PartialFactorToken)
                .ToString();

            return BuildOpenIdClientAuthenticatedRequest(content, request, "/authentication/certificate");
        }

        private static HttpRequestMessage BuildOpenIdClientAuthenticatedRequest(string formContent, ClientAuthenticatedRequest clientAuthenticatedRequest, string uri = "/connect/token")
        {
            return new HttpRequestMessage(HttpMethod.Post, new Uri(uri, UriKind.Relative))
                .WithAcceptHeader("application/json")
                .WithBasicAuthorizationHeader(clientAuthenticatedRequest.ClientId, clientAuthenticatedRequest.ClientSecret)
                .WithContent(formContent, "application/x-www-form-urlencoded");
        }
    }
}