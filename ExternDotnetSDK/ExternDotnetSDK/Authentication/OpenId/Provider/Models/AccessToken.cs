using System;
using Vostok.Commons.Time;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider.Models
{
    internal class AccessToken
    {
        private readonly string value;
        private readonly TimeBudget timeToLive;

        public AccessToken(string value, string refreshToken, TimeBudget timeToLive)
        {
            RefreshToken = refreshToken;
            this.value = value;
            this.timeToLive = timeToLive;
        }

        public string RefreshToken { get; }

        public bool HasNotExpired => timeToLive.HasExpired;

        public bool WillExpireAfter(TimeSpan interval) => timeToLive.Remaining < interval;

        public override string ToString() => value;
    }
}