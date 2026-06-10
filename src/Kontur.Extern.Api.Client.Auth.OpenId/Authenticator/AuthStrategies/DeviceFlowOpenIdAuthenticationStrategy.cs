#nullable enable
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.DeviceFlowUserInteraction;
using Kontur.Extern.Api.Client.Auth.OpenId.Client;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses;
using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.AuthStrategies
{
    internal class DeviceFlowOpenIdAuthenticationStrategy : IOpenIdAuthenticationStrategy
    {
        private readonly IDeviceFlowUserInteractionProvider userInteractionProvider;

        public DeviceFlowOpenIdAuthenticationStrategy(IDeviceFlowUserInteractionProvider userInteractionProvider)
        {
            this.userInteractionProvider = userInteractionProvider;
        }

        public async Task<TokenResponse> AuthenticateAsync(IOpenIdClient openId, OpenIdAuthenticationOptions options, TimeSpan? timeout = null)
        {
            var startResponse = await StartDeviceFlowAuthorizationAsync(openId, options, timeout).ConfigureAwait(false);
            var interval = TimeSpan.FromSeconds(startResponse.IntervalInSeconds);

            using var ui = InitiateUserInteraction(startResponse);

            while (true)
            {
                CheckNotCancelled(ui);

                var tokenResponse = await TryGetTokenAsync(openId, options, startResponse.DeviceCode, timeout).ConfigureAwait(false);
                if (tokenResponse is not null)
                    return tokenResponse;

                CheckNotCancelled(ui);

                await Task.Delay(interval).ConfigureAwait(false);
            }

            static void CheckNotCancelled(IDeviceFlowUserInteractionProcess userInteractionProcess)
            {
                if (userInteractionProcess.IsCancelled())
                    throw Errors.DeviceFlowAuthorizationCancelledByUser();
            }
        }

        private static async Task<TokenResponse?> TryGetTokenAsync(
            IOpenIdClient openId,
            OpenIdAuthenticationOptions options,
            string deviceCode,
            TimeSpan? timeout)
        {
            try
            {
                var request = new DeviceTokenRequest(
                    deviceCode,
                    options.Scope,
                    options.ClientId,
                    options.ApiKey);

                return await openId.RequestTokenAsync(request, timeout).ConfigureAwait(false);
            }
            catch (OpenIdException ex) when (IsSkippableError(ex))
            {
                return null;
            }

            static bool IsSkippableError(OpenIdException ex)
            {
                return ex.ServerErrorCode
                        is OpenIdServerErrorCode.AuthorizationPending
                           or OpenIdServerErrorCode.SlowDown;
            }
        }

        private static Task<DeviceAuthenticationResponse> StartDeviceFlowAuthorizationAsync(
            IOpenIdClient openId,
            OpenIdAuthenticationOptions options,
            TimeSpan? timeout)
        {
            var request = new StartDeviceAuthenticationRequest(
                options.Scope,
                options.ClientId,
                options.ApiKey);

            return openId.StartDeviceAuthenticationAsync(request, timeout);
        }

        private IDeviceFlowUserInteractionProcess InitiateUserInteraction(
            DeviceAuthenticationResponse startResponse)
        {
            var info = new DeviceFlowUserInteractionInfo
            {
                VerificationUriComplete = startResponse.VerificationUriComplete,
                ForcedUserReloginVerificationUri = startResponse.VerificationUriComplete + "&prompt=login",
                ExpiresIn = TimeSpan.FromSeconds(startResponse.ExpiresInSeconds),
            };
            return userInteractionProvider.InitiateUserInteraction(info);
        }
    }
}