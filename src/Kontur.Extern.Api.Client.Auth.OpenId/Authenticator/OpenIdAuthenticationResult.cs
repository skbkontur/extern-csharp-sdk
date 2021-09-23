using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Common.Time;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator
{
    internal class OpenIdAuthenticationResult : IAuthenticationResult
    {
        public OpenIdAuthenticationResult(string accessToken, TimeInterval remainingTime)
        {
            AccessToken = accessToken;
            RemainingTime = remainingTime;
        }

        public string AccessToken { get; }
        public TimeInterval RemainingTime { get; }

        public Request Apply(Request request) => request.WithAuthorizationHeader("Bearer", AccessToken); 
    }
}