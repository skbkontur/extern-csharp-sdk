#nullable enable
using System;
using Kontur.Extern.Client.Authentication.OpenId.Time;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider.Models
{
    internal class AccessToken
    {
        private readonly string value;
        private readonly ITimeToLive timeToLive;
        
        public AccessToken(string value, string refreshToken, ITimeToLive timeToLive)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(value));
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(refreshToken));
            if (ReferenceEquals(timeToLive, null))
                throw new ArgumentNullException(nameof(timeToLive));
            if (timeToLive.HasExpired)
                throw Errors.AccessTokenAlreadyExpired(nameof(timeToLive));
            
            RefreshToken = refreshToken;
            this.value = value;
            this.timeToLive = timeToLive;
        }

        public string RefreshToken { get; }

        public bool HasNotExpired => !timeToLive.HasExpired;

        public bool WillExpireAfter(TimeSpan interval) => timeToLive.WillExpireAfter(interval);

        public override string ToString() => value;
    }
}