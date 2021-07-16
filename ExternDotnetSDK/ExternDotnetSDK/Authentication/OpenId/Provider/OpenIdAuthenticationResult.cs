using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider
{
    internal class OpenIdAuthenticationResult : IAuthenticationResult
    {
        public OpenIdAuthenticationResult(string accessToken) => AccessToken = accessToken;

        public string AccessToken { get; }

        public Request Apply(Request request) => request.WithAuthorizationHeader("Bearer", AccessToken); 
    }
}