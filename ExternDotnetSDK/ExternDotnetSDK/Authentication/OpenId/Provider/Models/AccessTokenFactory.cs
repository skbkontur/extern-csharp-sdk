#nullable enable
using System;
using System.Diagnostics;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;
using Vostok.Commons.Time;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider.Models
{
    internal class AccessTokenFactory
    {
        private readonly Stopwatch stopwatch;
        
        public AccessTokenFactory() => stopwatch = Stopwatch.StartNew();

        /// <summary>
        /// Create an access token from the response <paramref name="tokenResponse" /> or return null if the token from the response has already expired.  
        /// </summary>
        /// <param name="tokenResponse">A response with an access token</param>
        /// <returns></returns>
        public AccessToken? TryCreate(TokenResponse tokenResponse)
        {
            var value = tokenResponse.AccessToken;
            var refreshToken = tokenResponse.RefreshToken;
            var leftInterval = TimeSpan.FromSeconds(tokenResponse.ExpiresInSeconds) - stopwatch.Elapsed;
            if (leftInterval <= TimeSpan.Zero)
                return null;

            return new AccessToken(value, refreshToken, TimeBudget.StartNew(leftInterval));
        }
    }
}