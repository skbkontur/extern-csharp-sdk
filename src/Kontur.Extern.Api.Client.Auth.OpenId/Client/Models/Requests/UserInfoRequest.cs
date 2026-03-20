using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests
{
    public abstract class UserInfoRequest : ClientAuthenticatedRequest
    {
        protected UserInfoRequest(string accessToken, string clientId, string clientSecret)
            : base(clientId, clientSecret)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(accessToken));

            AccessToken = accessToken;
        }

        public string AccessToken { get; }
    }
}