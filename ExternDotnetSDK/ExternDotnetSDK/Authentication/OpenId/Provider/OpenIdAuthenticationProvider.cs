using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Authentication.OpenId.Client;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Authentication.OpenId.Exceptions;
using Kontur.Extern.Client.Authentication.OpenId.Provider.AuthStrategies;
using Kontur.Extern.Client.Authentication.OpenId.Provider.Models;
using Kontur.Extern.Client.Authentication.OpenId.Time;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider
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
        
        public async Task<IAuthenticationResult> AuthenticateAsync(TimeSpan? timeout = null)
        {
            // NOTE: if two threads will do the same operation it's not a problem -- eventually there will be only one token.
            //       if getting two authentications/refreshments is very inefficient or results with errors,
            //       there should be somehow async locking (e.g. SemaphoreSlim)     
            if (context.TryGetAccessToken(out var token) && token.HasNotExpired)
            {
                if (token.WillExpireAfter(options.ProactiveAuthTokenRefreshInterval))
                {
                    var accessTokenFactory = new AccessTokenFactory(stopwatchFactory);
                    var tokenResponse = await RefreshTokenAsync(token, timeout).ConfigureAwait(false);
                    token = CreateAccessToken(accessTokenFactory, tokenResponse);
                    context.SetAccessToken(token);
                }
            }
            else
            {
                var accessTokenFactory = new AccessTokenFactory(stopwatchFactory);
                var tokenResponse = await authenticationStrategy.AuthenticateAsync(openId, options, timeout).ConfigureAwait(false);
                token = CreateAccessToken(accessTokenFactory, tokenResponse);
                context.SetAccessToken(token);
            }

            return new OpenIdAuthenticationResult(token.ToString());

            static AccessToken CreateAccessToken(AccessTokenFactory accessTokenFactory, TokenResponse tokenResponse)
            {
                var token = accessTokenFactory.CreateIfNotExpired(tokenResponse);
                if (token == null)
                    throw OpenIdErrors.AuthTokenHasAlreadyExpired();
                return token;
            }
        }

        private Task<TokenResponse> RefreshTokenAsync(AccessToken accessToken, TimeSpan? timeout) =>
            openId.RequestTokenAsync(
                new RefreshTokenRequest
                {
                    ClientId = options.ClientId,
                    ClientSecret = options.ApiKey,
                    RefreshToken = accessToken.RefreshToken,
                    Scope = options.Scope
                },
                timeout
            );
    }
}