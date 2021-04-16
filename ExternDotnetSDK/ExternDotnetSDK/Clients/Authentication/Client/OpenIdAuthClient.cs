using System;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication.Client.Factories;
using Kontur.Extern.Client.Clients.Authentication.Client.Models;
using Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests;
using Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Responses;
using Kontur.Extern.Client.Clients.Authentication.Providers;
using Kontur.Extern.Client.Clients.Common.Logging;

namespace Kontur.Extern.Client.Clients.Authentication.Client
{
    internal class OpenIdAuthClient : BaseOpenIdProvider
    {
        private readonly Sender sender;

        public OpenIdAuthClient(string baseUrl, ILogger log)
        {
            var client = new HttpClient {BaseAddress = new Uri(baseUrl, UriKind.Relative)};
            sender = new Sender(client, log);
        }

        public Task<ServiceResult<TokenResponse, ErrorResponse>> RequestTokenAsync(RefreshTokenRequest tokenRequest, TimeSpan? timeout = null)
        {
            var request = RefreshTokenRequest(tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest)));
            return sender.SendAsync<TokenResponse, ErrorResponse>(request, timeout);
        }

        public Task<ServiceResult<TokenResponse, ErrorResponse>> RequestTokenAsync(PasswordTokenRequest tokenRequest, TimeSpan? timeout = null)
        {
            var request = PasswordTokenRequest(tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest)));
            return sender.SendAsync<TokenResponse, ErrorResponse>(request, timeout);
        }

        public Task<ServiceResult<TokenResponse, ErrorResponse>> RequestTokenAsync(CertificateTokenRequest tokenRequest, TimeSpan? timeout = null)
        {
            var request = CertificateTokenRequest(tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest)));
            return sender.SendAsync<TokenResponse, ErrorResponse>(request, timeout);
        }

        public Task<ServiceResult<TokenResponse, ErrorResponse>> RequestTokenAsync(TrustedTokenRequest tokenRequest, TimeSpan? timeout = null)
        {
            var request = TrustedTokenRequest(tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest)));
            return sender.SendAsync<TokenResponse, ErrorResponse>(request, timeout);
        }

        public Task<ServiceResult<CertificateAuthenticationResponse, ErrorResponse>> CertificateAuthenticationAsync(
            CertificateAuthenticationRequest certificateRequest,
            TimeSpan? timeout = null)
        {
            var request = CertificateAuthenticationRequest(certificateRequest ?? throw new ArgumentNullException(nameof(certificateRequest)));
            return sender.SendAsync<CertificateAuthenticationResponse, ErrorResponse>(request, timeout);
        }
    }
}