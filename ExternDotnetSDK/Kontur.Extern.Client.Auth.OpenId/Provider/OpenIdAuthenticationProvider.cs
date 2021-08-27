#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Auth.Abstractions;
using Kontur.Extern.Client.Auth.OpenId.Client;
using Kontur.Extern.Client.Auth.OpenId.Client.Models.Requests;
using Kontur.Extern.Client.Auth.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Auth.OpenId.Provider.AuthStrategies;
using Kontur.Extern.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Client.Common.Time;

namespace Kontur.Extern.Client.Auth.OpenId.Provider
{
    internal class OpenIdAuthenticationProvider : IAuthenticationProvider
    {
        private readonly OpenIdAuthenticationOptions options;
        private readonly IOpenIdClient openId;
        private readonly IOpenIdAuthenticationStrategy authenticationStrategy;
        private readonly OpenIdAuthenticationContext context = new();
        private readonly IStopwatchFactory stopwatchFactory;

        public OpenIdAuthenticationProvider(
            OpenIdAuthenticationOptions options, 
            IOpenIdClient openId,
            IOpenIdAuthenticationStrategy authenticationStrategy,
            IStopwatchFactory stopwatchFactory)
        {
            this.options = options;
            this.openId = openId;
            this.authenticationStrategy = authenticationStrategy;
            this.stopwatchFactory = stopwatchFactory;
        }
        
        public async Task<IAuthenticationResult> AuthenticateAsync(bool force = false, TimeSpan? timeout = null)
        {
            // NOTE: if two threads will do the same operation it's not a problem -- eventually there will be only one token.
            //       if getting two authentications/refreshments is very inefficient or results with errors,
            //       there should be somehow async locking (e.g. SemaphoreSlim)
            AccessToken? accessToken = null;
            if (!force && context.TryGetAccessToken(out accessToken))
            {
                accessToken = await ActualizeCurrentAccessTokenAsync(accessToken).ConfigureAwait(false);
            }
            accessToken ??= await ObtainNewAccessTokenAsync().ConfigureAwait(false);

            context.SetAccessToken(accessToken);
            return new OpenIdAuthenticationResult(accessToken.ToString(), accessToken.RemainingTime);

            async Task<AccessToken?> ActualizeCurrentAccessTokenAsync(AccessToken token)
            {
                if (token.HasExpired)
                    return null;

                if (!token.WillExpireAfter(options.ProactiveAuthTokenRefreshInterval))
                    return token;

                if (!token.TryGetRefreshToken(out var refreshToken))
                    return null;

                var accessTokenFactory = new AccessTokenFactory(stopwatchFactory);
                var tokenResponse = await RefreshTokenAsync(refreshToken, timeout).ConfigureAwait(false);
                return accessTokenFactory.CreateAccessToken(tokenResponse);
            }

            async Task<AccessToken> ObtainNewAccessTokenAsync()
            {
                var accessTokenFactory = new AccessTokenFactory(stopwatchFactory);
                var tokenResponse = await authenticationStrategy.AuthenticateAsync(openId, options, timeout).ConfigureAwait(false);
                return accessTokenFactory.CreateAccessToken(tokenResponse);
            }
        }

        private Task<TokenResponse> RefreshTokenAsync(string refreshToken, TimeSpan? timeout)
        {
            var request = new RefreshTokenRequest(
                refreshToken,
                options.ClientId,
                options.ApiKey,
                options.Scope
            );
            return openId.RequestTokenAsync(request, timeout);
        }
    }
}