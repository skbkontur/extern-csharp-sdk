#nullable enable
using System;
using Kontur.Extern.Client.Authentication.OpenId.Time;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider.Models
{
    internal class AccessToken
    {
        private readonly string value;
        private readonly string? refreshToken;
        private readonly ITimeToLive timeToLive;
        
        public AccessToken(string value, string? refreshToken, ITimeToLive timeToLive)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(value));
            if (refreshToken != null && string.IsNullOrWhiteSpace(refreshToken))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(refreshToken));
            
            if (ReferenceEquals(timeToLive, null))
                throw new ArgumentNullException(nameof(timeToLive));
            if (timeToLive.HasExpired)
                throw Errors.AccessTokenAlreadyExpired(nameof(timeToLive));
            
            this.value = value;
            this.refreshToken = refreshToken;
            this.timeToLive = timeToLive;
        }

        public bool TryGetRefreshToken(out string token)
        {
            if (refreshToken != null)
            {
                token = refreshToken;
                return true;
            }

            token = default!;
            return false;
        }

        public bool HasNotExpired => !timeToLive.HasExpired;
        public bool HasExpired => timeToLive.HasExpired;
        public TimeInterval RemainingTime => timeToLive.Remaining;

        public bool WillExpireAfter(TimeInterval interval) => timeToLive.WillExpireAfter(interval);

        public override string ToString() => value;
    }
}