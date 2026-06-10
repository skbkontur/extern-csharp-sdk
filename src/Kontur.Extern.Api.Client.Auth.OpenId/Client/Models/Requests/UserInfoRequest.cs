using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests
{
    public class UserInfoRequest
    {
        public UserInfoRequest(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(accessToken));

            AccessToken = accessToken;
        }

        public string AccessToken { get; }
    }
}