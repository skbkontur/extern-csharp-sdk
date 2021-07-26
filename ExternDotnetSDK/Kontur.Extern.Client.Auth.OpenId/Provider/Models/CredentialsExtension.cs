using Kontur.Extern.Client.Http.Models;

namespace Kontur.Extern.Client.Auth.OpenId.Provider.Models
{
    internal static class CredentialsExtension
    {
        public static Base64String ToBasicAuthenticationParameter(this Credentials credentials) =>
            Base64String.Encode(credentials.UserName + ":" + credentials.Password);
    }
}