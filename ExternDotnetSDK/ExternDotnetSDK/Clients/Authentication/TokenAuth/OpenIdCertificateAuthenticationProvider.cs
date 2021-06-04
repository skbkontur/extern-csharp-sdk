using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication.Client;
using Kontur.Extern.Client.Clients.Authentication.Client.Models;
using Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests;
using Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Responses;
using Kontur.Extern.Client.Clients.Authentication.Providers;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Cryptography;

namespace Kontur.Extern.Client.Clients.Authentication.TokenAuth
{
    // ReSharper disable CommentTypo
    namespace Kontur.Extern.Client.Clients.Authentication
    {
        public class OpenIdCertificateAuthenticationProvider : IAuthenticationProvider
        {
            private readonly CertificateAuthenticationRequest certificateAuthenticationRequest;
            private readonly ILogger log;
            private readonly OpenIdAuthClient authClient;
            private readonly ICrypt cryptoProvider;
            public  TokenResponse CurrentResponse { get; protected set; }
            private DateTime lastTokenUpdate;

            public OpenIdCertificateAuthenticationProvider(string authenticationBaseAddress, CertificateAuthenticationRequest certificateAuthenticationRequest, ICrypt cryptoProvider, ILogger log = null)
            {
                this.log = log ?? new SilentLogger();
                this.authClient = new OpenIdAuthClient(authenticationBaseAddress, this.log);
                this.certificateAuthenticationRequest = certificateAuthenticationRequest;
                this.cryptoProvider = cryptoProvider;
            }

            private async Task<ServiceResult<TokenResponse, ErrorResponse>> AuthAsync(TimeSpan? timeout = null)
            {
                var authResponse = await authClient.CertificateAuthenticationAsync(certificateAuthenticationRequest, timeout);

                if (authResponse.Status != ServiceResultStatus.Success)
                    if (authResponse.ServiceResponseCode != null)
                        return ServiceResult<TokenResponse, ErrorResponse>.CreateServiceError(authResponse.ServiceError, (int) authResponse.ServiceResponseCode, authResponse.ErrorMessage);
                var decryptedKey = cryptoProvider.Decrypt(authResponse.Response.EncryptedKey);

                var certTokenRequest = new CertificateTokenRequest()
                {
                    ClientId = certificateAuthenticationRequest.ClientId,
                    ClientSecret = certificateAuthenticationRequest.ClientSecret,
                    DecryptedKey = decryptedKey,
                    Thumbprint = certificateAuthenticationRequest.PublicKey.Thumbprint
                };

                var tokenResponse = await authClient.RequestTokenAsync(certTokenRequest, timeout);
                return tokenResponse;
            }

            //TODO вынести в декаратор @gangboss
            private async Task<ServiceResult<TokenResponse, ErrorResponse>> RefreshAsync(TimeSpan? timeout = null)
            {
                var refreshToken = new RefreshTokenRequest()
                {
                    ClientId = certificateAuthenticationRequest.ClientId,
                    ClientSecret = certificateAuthenticationRequest.ClientSecret,
                    RefreshToken = CurrentResponse.RefreshToken
                };

                var authResponse = await authClient.RequestTokenAsync(refreshToken, timeout);
                return authResponse;
            }

            //TODO вынести в декаратор @gangboss
            public async Task<ServiceResult> AuthenticateAsync(TimeSpan? timeout = null)
            {
                if (CurrentResponse != null && lastTokenUpdate.AddSeconds(CurrentResponse.ExpiresIn) < DateTime.Now)
                {
                    var refreshResult = await RefreshAsync(timeout);
                    if (!refreshResult.Success)
                        return refreshResult;
                    CurrentResponse = refreshResult.Response;
                    lastTokenUpdate = DateTime.Now;

                    return refreshResult;
                }

                var authResult = await AuthAsync(timeout);

                if (!authResult.Success)
                    return authResult;
                CurrentResponse = authResult.Response;
                lastTokenUpdate = DateTime.Now;

                return authResult;
            }

            public Request ApplyAuth(Request request)
            {
                return request.WithHeader(SenderConstants.Authorization, "Bearer " + CurrentResponse.AccessToken);
            }
        }
    }
}