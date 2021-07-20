using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Authentication.OpenId.Exceptions;
using Kontur.Extern.Client.Authentication.OpenId.Provider.Models;
using Kontur.Extern.Client.HttpLevel;
using Kontur.Extern.Client.HttpLevel.Constants;
using Kontur.Extern.Client.HttpLevel.Contents;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Authentication.OpenId.Client
{
    public class OpenIdClient : IOpenIdClient
    {
        private readonly IHttpRequestsFactory http;
        private readonly ILog log;

        public OpenIdClient(IHttpRequestsFactory http, ILog log)
        {
            this.http = http;
            this.log = log;
        }
        
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
                .AddEntry(ContractConstants.CertificateAuthenticationRequest.PublicKey, request.PublicKey.GetRawCertData())
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
            var httpResponse = await httpRequest.TrySendAsync(timeout).ConfigureAwait(false);

            if (!httpResponse.Status.IsSuccessful)
            {
                if (httpResponse.Status.IsBadRequest && httpResponse.TryGetMessage<ErrorResponse>(out var errorResponse))
                {
                    throw new OpenIdException(errorResponse);
                }

                httpResponse.Status.EnsureSuccess();
            }

            return httpResponse.GetMessage<TResult>();
        }
    }
}