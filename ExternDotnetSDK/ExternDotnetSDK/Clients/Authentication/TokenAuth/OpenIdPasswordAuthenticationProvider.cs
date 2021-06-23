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

namespace Kontur.Extern.Client.Clients.Authentication.TokenAuth
{
    namespace Kontur.Extern.Client.Clients.Authentication
    {
        public class OpenIdPasswordAuthenticationProvider : IAuthenticationProvider
        {
            private readonly ILogger log;
            private readonly OpenIdAuthClient authClient;
            private readonly PasswordTokenRequest passwordTokenRequest;
            public TokenResponse CurrentResponse { get; private set; }
            private DateTime lastTokenUpdate;

            public OpenIdPasswordAuthenticationProvider(string authenticationBaseAddress, PasswordTokenRequest passwordTokenRequest, ILogger log = null)
            {
                this.passwordTokenRequest = passwordTokenRequest;
                this.log = log ?? new SilentLogger();
                this.authClient = new OpenIdAuthClient(authenticationBaseAddress, this.log);
            }

            protected async Task<ServiceResult<TokenResponse, ErrorResponse>> AuthAsync(TimeSpan? timeout = null)
            {
                var tokenResponse = await authClient.RequestTokenAsync(passwordTokenRequest);
                if (!tokenResponse.Success)
                    return tokenResponse;
                CurrentResponse = tokenResponse.Response;
                lastTokenUpdate = DateTime.Now;
                return tokenResponse;
            }

            //TODO вынести в декаратор @gangboss
            private async Task<ServiceResult<TokenResponse, ErrorResponse>> RefreshAsync(TimeSpan? timeout = null)
            {
                var refreshToken = new RefreshTokenRequest()
                {
                    ClientId = passwordTokenRequest.ClientId,
                    ClientSecret = passwordTokenRequest.ClientSecret,
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