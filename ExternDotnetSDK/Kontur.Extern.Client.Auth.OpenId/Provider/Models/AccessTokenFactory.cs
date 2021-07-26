#nullable enable
using System;
using Kontur.Extern.Client.Auth.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Client.Common.Time;

namespace Kontur.Extern.Client.Auth.OpenId.Provider.Models
{
    internal class AccessTokenFactory
    {
        private readonly IStopwatch stopwatch;
        
        public AccessTokenFactory(IStopwatchFactory stopwatchFactory) => stopwatch = stopwatchFactory.Start();
        
        /// <summary>
        /// Create an access token from the response <paramref name="tokenResponse" /> or throws <see cref="OpenIdException"/> if the token from the response has already expired.  
        /// </summary>
        /// <param name="tokenResponse">A response with an access token</param>
        /// <returns></returns>
        /// <exception cref="OpenIdException">the token from the response has already expired.</exception>
        public AccessToken CreateAccessToken(TokenResponse tokenResponse)
        {
            var value = tokenResponse.AccessToken;
            var refreshToken = tokenResponse.RefreshToken;
            if (TimeToLive.TryCreateActive(TimeSpan.FromSeconds(tokenResponse.ExpiresInSeconds), stopwatch, out var timeToLive))
                return new AccessToken(value, refreshToken, timeToLive);
            
            throw Errors.AuthTokenHasAlreadyExpired();
        }
    }
}