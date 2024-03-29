using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Api.Client.Http.Configurations;
using Kontur.Extern.Api.Client.Http.Constants;
using Kontur.Extern.Api.Client.Http.Contents;
using Kontur.Extern.Api.Client.Http.Options;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.NamingPolicies;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client
{
    public class OpenIdClient : IOpenIdClient
    {
        private static readonly IJsonSerializer Serializer = new SystemTextJsonSerializerFactory()
            .WithNamingPolicy(new SnakeCaseNamingPolicy())
            .CreateSerializer();
        
        public static OpenIdClient Create(RequestTimeouts requestTimeouts, IHttpClientConfiguration clientConfiguration, ILog log)
        {
            var http = new HttpRequestFactory(
                clientConfiguration,
                requestTimeouts,
                null,
                HandleOpenIdErrorResponse,
                null,
                Serializer,
                log
            );
            return new OpenIdClient(http);
        }

        private readonly IHttpRequestFactory http;

        private OpenIdClient(IHttpRequestFactory http) => this.http = http;

        public async Task<TokenResponse> RequestTokenAsync(RefreshTokenRequest tokenRequest, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(tokenRequest.ClientId))
                throw new ArgumentNullException(nameof(tokenRequest.ClientId));

            if (string.IsNullOrWhiteSpace(tokenRequest.RefreshToken))
                throw new ArgumentNullException(nameof(tokenRequest.RefreshToken));
            
            var content = new FormUrlEncodedContent()
                .AddGrantType(ContractConstants.GrantTypes.RefreshToken)
                .AddEntry(ContractConstants.RefreshTokenRequest.RefreshToken, tokenRequest.RefreshToken)
                .AddScope(tokenRequest.Scope)
                .AddRequestAuthentication(tokenRequest);

            return await PostToOpenIdServerAsync<TokenResponse>("/connect/token", tokenRequest.ToRequestAuthCredentials(), content, timeout).ConfigureAwait(false);
        }

        public async Task<TokenResponse> RequestTokenAsync(PasswordTokenRequest request, TimeSpan? timeout = null)
        {
            var content = new FormUrlEncodedContent()
                .AddGrantType(ContractConstants.GrantTypes.Password)
                .AddScope(request.Scope)
                .AddEntry(ContractConstants.PasswordTokenRequest.UserName, request.UserName)
                .AddEntry(ContractConstants.PasswordTokenRequest.Password, request.Password)
                .AddEntryIfNotEmpty(ContractConstants.PasswordTokenRequest.PartialFactorToken, request.PartialFactorToken)
                .AddRequestAuthentication(request);

            return await PostToOpenIdServerAsync<TokenResponse>("/connect/token", request.ToRequestAuthCredentials(), content, timeout).ConfigureAwait(false);
        }

        public async Task<TokenResponse> RequestTokenAsync(CertificateTokenRequest request, TimeSpan? timeout = null)
        {
            var content = new FormUrlEncodedContent()
                .AddGrantType(ContractConstants.GrantTypes.Certificate)
                .AddScope(request.Scope)
                .AddEntryIfNotEmpty(ContractConstants.CertificateTokenRequest.DecryptedKey, Convert.ToBase64String(request.DecryptedKey))
                .AddEntryIfNotEmpty(ContractConstants.CertificateTokenRequest.Thumbprint, request.Thumbprint)
                .AddRequestAuthentication(request);

            return await PostToOpenIdServerAsync<TokenResponse>("/connect/token", request.ToRequestAuthCredentials(), content, timeout).ConfigureAwait(false);
        }

        public async Task<TokenResponse> RequestTokenAsync(JwtTrustedTokenRequest request, TimeSpan? timeout = null)
        {
            var content = new FormUrlEncodedContent()
                .AddGrantType(ContractConstants.GrantTypes.Trusted)
                .AddScope(request.Scope)
                .AddEntry(ContractConstants.TrustedTokenRequest.Token, request.Token)
                .AddRequestAuthentication(request);

            return await PostToOpenIdServerAsync<TokenResponse>("/connect/token", request.ToRequestAuthCredentials(), content, timeout).ConfigureAwait(false);
        }

        public async Task<CertificateAuthenticationResponse> CertificateAuthenticationAsync(CertificateAuthenticationRequest request, TimeSpan? timeout = null)
        {
            var content = new FormUrlEncodedContent()
                .AddEntry(ContractConstants.CertificateAuthenticationRequest.PublicKey, request.PublicKeyCertificate.GetRawCertData())
                .AddEntry(ContractConstants.CertificateAuthenticationRequest.IsFree, request.Free.ToString())
                .AddEntryIfNotEmpty(ContractConstants.CertificateAuthenticationRequest.PartialFactorToken, request.PartialFactorToken)
                .AddRequestAuthentication(request);

            return await PostToOpenIdServerAsync<CertificateAuthenticationResponse>("/authentication/certificate", request.ToRequestAuthCredentials(), content, timeout).ConfigureAwait(false);
        }

        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        private Task<TResult> PostToOpenIdServerAsync<TResult>(string url, Credentials basicAuthCredentials, FormUrlEncodedContent content, TimeSpan? timeout)
        {
            var httpRequest = http.Post(url)
                .WithPayload(content)
                .Accept(ContentTypes.Json)
                .Authorization(AuthSchemes.Basic, basicAuthCredentials.ToBasicAuthenticationParameter());
            return SendRequestAsync<TResult>(httpRequest, timeout);
        }

        private static async Task<TResult> SendRequestAsync<TResult>(IHttpRequest httpRequest, TimeSpan? timeout)
        {
            var httpResponse = await httpRequest.SendAsync(timeout).ConfigureAwait(false);
            return await httpResponse.GetMessageAsync<TResult>().ConfigureAwait(false);
        }

        private static async ValueTask<bool> HandleOpenIdErrorResponse(IHttpResponse httpResponse)
        {
            if (httpResponse.Status.IsBadRequest)
            {
                var errorResponse = await httpResponse.TryGetMessageAsync<ErrorResponse>().ConfigureAwait(false);
                if (errorResponse is not null)
                {
                    throw new OpenIdException(errorResponse);
                }
            }

            return false;
        }
    }
}