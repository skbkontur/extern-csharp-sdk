using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Authentication.OpenId.Exceptions;
using Kontur.Extern.Client.HttpLevel;
using Kontur.Extern.Client.HttpLevel.Options;
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
            
            var content = new FormUrlEncodedContentBuilder()
                .AddGrantType(ContractConstants.GrantTypes.RefreshToken)
                .Add(ContractConstants.RefreshTokenRequest.RefreshToken, tokenRequest.RefreshToken)
                .AddScope(tokenRequest.Scope);

            return await PostToOpenIdServerAsync<TokenResponse>("/connect/token", content, timeout).ConfigureAwait(false);
        }

        public async Task<TokenResponse> RequestTokenAsync(PasswordTokenRequest request, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId))
                throw new ArgumentNullException(nameof(request.ClientId));

            if (string.IsNullOrWhiteSpace(request.UserName))
                throw new ArgumentNullException(nameof(request.UserName));

            var content = new FormUrlEncodedContentBuilder()
                .AddGrantType(ContractConstants.GrantTypes.Password)
                .AddScope(request.Scope)
                .AddIfNotNull(ContractConstants.PasswordTokenRequest.UserName, request.UserName)
                .AddIfNotNull(ContractConstants.PasswordTokenRequest.Password, request.Password)
                .AddIfNotNull(ContractConstants.PasswordTokenRequest.PartialFactorToken, request.PartialFactorToken);

            return await PostToOpenIdServerAsync<TokenResponse>("/connect/token", content, timeout).ConfigureAwait(false);
        }

        public async Task<TokenResponse> RequestTokenAsync(CertificateTokenRequest request, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId))
                throw new ArgumentNullException(nameof(request.ClientId));

            if (string.IsNullOrWhiteSpace(request.Thumbprint))
                throw new ArgumentNullException(nameof(request.Thumbprint));

            if (request.DecryptedKey == null)
                throw new ArgumentNullException(nameof(request.DecryptedKey));

            var content = new FormUrlEncodedContentBuilder()
                .AddGrantType(ContractConstants.GrantTypes.Certificate)
                .AddScope(request.Scope)
                .AddIfNotNull(ContractConstants.CertificateTokenRequest.DecryptedKey, Convert.ToBase64String(request.DecryptedKey))
                .AddIfNotNull(ContractConstants.CertificateTokenRequest.Thumbprint, request.Thumbprint);

            return await PostToOpenIdServerAsync<TokenResponse>("/connect/token", content, timeout).ConfigureAwait(false);
        }

        public async Task<TokenResponse> RequestTokenAsync(TrustedTokenRequest request, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId))
                throw new ArgumentNullException(nameof(request.ClientId));

            if (string.IsNullOrWhiteSpace(request.Token))
                throw new ArgumentNullException(nameof(request.Token));

            var content = new FormUrlEncodedContentBuilder()
                .AddGrantType(ContractConstants.GrantTypes.Trusted)
                .AddScope(request.Scope)
                .Add(ContractConstants.TrustedTokenRequest.Token, request.Token);

            return await PostToOpenIdServerAsync<TokenResponse>("/connect/token", content, timeout).ConfigureAwait(false);
        }

        public async Task<CertificateAuthenticationResponse> CertificateAuthenticationAsync(CertificateAuthenticationRequest request, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(request.ClientId))
                throw new ArgumentNullException(nameof(request.ClientId));

            if (request.PublicKey == null)
                throw new ArgumentNullException(nameof(request.PublicKey));

            var content = new FormUrlEncodedContentBuilder()
                .Add(
                    ContractConstants.CertificateAuthenticationRequest.PublicKey,
                    Convert.ToBase64String(request.PublicKey.GetRawCertData()))
                .Add(ContractConstants.CertificateAuthenticationRequest.IsFree, request.Free.ToString())
                .AddIfNotNull(ContractConstants.CertificateAuthenticationRequest.PartialFactorToken, request.PartialFactorToken);

            return await PostToOpenIdServerAsync<CertificateAuthenticationResponse>("/authentication/certificate", content, timeout).ConfigureAwait(false);
        }

        private async Task<TResult> PostToOpenIdServerAsync<TResult>(string url, FormUrlEncodedContentBuilder content, TimeSpan? timeout)
        {
            var httpRequest = http.Post(url)
                .WithFormUrlEncoded(content.ToString())
                .Accept(ContentTypes.Json);
            return await SendRequestAsync<TResult>(httpRequest, timeout).ConfigureAwait(false);
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