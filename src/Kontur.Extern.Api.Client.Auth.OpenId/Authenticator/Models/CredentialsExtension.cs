using Kontur.Extern.Api.Client.Http.Models;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models
{
    internal static class CredentialsExtension
    {
        public static Base64String ToBasicAuthenticationParameter(this Credentials credentials) =>
            Base64String.Encode(credentials.UserName + ":" + credentials.Password);
    }
}