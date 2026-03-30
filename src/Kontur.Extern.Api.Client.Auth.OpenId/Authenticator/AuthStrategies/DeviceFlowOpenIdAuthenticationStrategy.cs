using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.DeviceFlowUI;
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
        private readonly IDeviceFlowUIProvider _deviceFlowUi;

        public DeviceFlowOpenIdAuthenticationStrategy(IDeviceFlowUIProvider deviceFlowUi)
        {
            _deviceFlowUi = deviceFlowUi;
        }

        public async Task<TokenResponse> AuthenticateAsync(IOpenIdClient openId, OpenIdAuthenticationOptions options, TimeSpan? timeout = null)
        {
            var startResponse = await StartDeviceFlowAuthorizationAsync(openId, options, timeout).ConfigureAwait(false);
            var interval = TimeSpan.FromSeconds(startResponse.IntervalInSeconds);

            using var ui = ShowUI(startResponse);

            while (true)
            {
                CheckNotCancelled(ui);

                var tokenResponse = await TryGetTokenAsync(openId, options, startResponse.DeviceCode, timeout).ConfigureAwait(false);
                if (tokenResponse != null)
                    return tokenResponse;

                CheckNotCancelled(ui);

                await Task.Delay(interval).ConfigureAwait(false);
            }

            static void CheckNotCancelled(IDeviceFlowUI ui)
            {
                if (ui.IsCancelled())
                    throw new OpenIdException("cancelled_by_user");
            }
        }

        private async Task<TokenResponse> TryGetTokenAsync(IOpenIdClient openId, OpenIdAuthenticationOptions options, string deviceCode, TimeSpan? timeout)
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

            static bool IsSkippableError(OpenIdException exception)
            {
                // todo: add exception field ?
                return exception.Message.Contains("authorization_pending")
                       || exception.Message.Contains("slow_down");
            }
        }

        private Task<DeviceAuthenticationResponse> StartDeviceFlowAuthorizationAsync(IOpenIdClient openId, OpenIdAuthenticationOptions options, TimeSpan? timeout)
        {
            var request = new StartDeviceAuthenticationRequest(
                options.Scope,
                options.ClientId,
                options.ApiKey);

            return openId.StartDeviceAuthenticationAsync(request, timeout);
        }

        private IDeviceFlowUI ShowUI(DeviceAuthenticationResponse startResponse)
        {
            var info = new DeviceFlowInfo
            {
                AuthorizationUri = startResponse.VerificationUriComplete,
                UriForChangeUser = startResponse.VerificationUriComplete + "&prompt=login",
                ExpiresIn = TimeSpan.FromSeconds(startResponse.ExpiresInSeconds),
            };
            return _deviceFlowUi.ShowUI(info);
        }
    }
}