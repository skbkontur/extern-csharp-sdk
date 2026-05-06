#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.AuthStrategies;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.Auth.OpenId.Client;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator
{
    internal class OpenIdAuthenticator : IAuthenticator
    {
        private readonly OpenIdAuthenticationOptions options;
        private readonly IOpenIdClient openId;
        private readonly IOpenIdAuthenticationStrategy authenticationStrategy;
        private readonly IOpenIdAuthenticationContext authenticationContext;
        private readonly IStopwatchFactory stopwatchFactory;

        public OpenIdAuthenticator(
            OpenIdAuthenticationOptions options, 
            IOpenIdClient openId,
            IOpenIdAuthenticationStrategy authenticationStrategy,
            IOpenIdAuthenticationContext authenticationContext,
            IStopwatchFactory stopwatchFactory)
        {
            this.options = options;
            this.openId = openId;
            this.authenticationStrategy = authenticationStrategy;
            this.authenticationContext = authenticationContext;
            this.stopwatchFactory = stopwatchFactory;
        }
        
        public async Task<IAuthenticationResult> AuthenticateAsync(bool force = false, TimeSpan? timeout = null)
        {
            // NOTE: if two threads will do the same operation it's not a problem -- eventually there will be only one token.
            //       if getting two authentications/refreshments is very inefficient or results with errors,
            //       there should be somehow async locking (e.g. SemaphoreSlim)
            IAccessToken? accessToken = null;
            if (!force && authenticationContext.TryGetAccessToken(out accessToken))
            {
                accessToken = await ActualizeCurrentAccessTokenAsync(accessToken).ConfigureAwait(false);
            }
            accessToken ??= await ObtainNewAccessTokenAsync().ConfigureAwait(false);

            authenticationContext.SetAccessToken(accessToken);
            return new OpenIdAuthenticationResult(accessToken.ToString(), accessToken.RemainingTime);

            async Task<IAccessToken?> ActualizeCurrentAccessTokenAsync(IAccessToken token)
            {
                if (token.HasNotExpired
                    && !token.WillExpireAfter(options.ProactiveAuthTokenRefreshInterval))
                    return token;

                if (!token.TryGetRefreshToken(out var refreshToken))
                    return null;

                var accessTokenFactory = new AccessTokenFactory(stopwatchFactory);
                var tokenResponse = await TryRefreshTokenAsync(refreshToken, timeout).ConfigureAwait(false);
                if (tokenResponse is not null)
                    return accessTokenFactory.CreateAccessToken(tokenResponse);

                return null;
            }

            async Task<IAccessToken> ObtainNewAccessTokenAsync()
            {
                var accessTokenFactory = new AccessTokenFactory(stopwatchFactory);
                var tokenResponse = await authenticationStrategy.AuthenticateAsync(openId, options, timeout).ConfigureAwait(false);
                return accessTokenFactory.CreateAccessToken(tokenResponse);
            }
        }

        private async Task<TokenResponse?> TryRefreshTokenAsync(string refreshToken, TimeSpan? timeout)
        {
            try
            {
                var request = new RefreshTokenRequest(
                    refreshToken,
                    options.ClientId,
                    options.ApiKey,
                    options.Scope
                );
                return await openId.RequestTokenAsync(request, timeout).ConfigureAwait(false);
            }
            catch (OpenIdException)
            {
                return null;
            }
        }

        public async Task<UserInfo> GetCurrentSessionUserInfoAsync(TimeSpan? timeout)
        {
            if (!authenticationContext.TryGetAccessToken(out var accessToken))
                throw Errors.UserNotAuthenticatedYet();

            var request = new UserInfoRequest(accessToken.ToString());

            var response = await openId.GetUserInfoAsync(request, timeout).ConfigureAwait(false);

            return new UserInfo
            {
                Sub = response.Sub,
                GivenName = response.GivenName,
                FamilyName = response.FamilyName,
                MiddleName = response.MiddleName,
                Name = response.Name,
            };
        }
    }
}