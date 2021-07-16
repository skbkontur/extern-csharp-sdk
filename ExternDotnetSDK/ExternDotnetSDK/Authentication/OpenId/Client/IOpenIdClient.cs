using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;

namespace Kontur.Extern.Client.Authentication.OpenId.Client
{
    public interface IOpenIdClient
    {
        Task<TokenResponse> RequestTokenAsync(RefreshTokenRequest tokenRequest, TimeSpan? timeout = null);

        Task<TokenResponse> RequestTokenAsync(PasswordTokenRequest request, TimeSpan? timeout = null);

        Task<TokenResponse> RequestTokenAsync(CertificateTokenRequest request, TimeSpan? timeout = null);

        Task<TokenResponse> RequestTokenAsync(JwtTrustedTokenRequest request, TimeSpan? timeout = null);

        Task<CertificateAuthenticationResponse> CertificateAuthenticationAsync(CertificateAuthenticationRequest request, TimeSpan? timeout = null);
    }
}