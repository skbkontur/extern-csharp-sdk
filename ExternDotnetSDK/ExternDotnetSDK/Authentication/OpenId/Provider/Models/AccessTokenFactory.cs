#nullable enable
using System;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Authentication.OpenId.Time;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider.Models
{
    internal class AccessTokenFactory
    {
        private readonly IStopwatch stopwatch;
        
        public AccessTokenFactory(IStopwatchFactory stopwatchFactory) => stopwatch = stopwatchFactory.Start();
        
        /// <summary>
        /// Create an access token from the response <paramref name="tokenResponse" /> or return null if the token from the response has already expired.  
        /// </summary>
        /// <param name="tokenResponse">A response with an access token</param>
        /// <returns></returns>
        public AccessToken? CreateIfNotExpired(TokenResponse tokenResponse)
        {
            var value = tokenResponse.AccessToken;
            var refreshToken = tokenResponse.RefreshToken;
            return TimeToLive.TryCreateActive(TimeSpan.FromSeconds(tokenResponse.ExpiresInSeconds), stopwatch, out var timeToLive) 
                ? new AccessToken(value, refreshToken, timeToLive) 
                : null;
        }
    }
}