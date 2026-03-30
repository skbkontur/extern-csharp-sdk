#nullable enable
namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models
{
    internal class OpenIdAuthenticationContext : IOpenIdAuthenticationContext
    {
        private readonly object syncObject = new();
        private IAccessToken? accessToken;

        public bool TryGetAccessToken(out IAccessToken token)
        {
            lock (syncObject)
            {
                if (accessToken == null)
                {
                    token = default!;
                    return false;
                }

                token = accessToken;
                return true;
            }
        }

        public void SetAccessToken(IAccessToken token)
        {
            lock (syncObject)
            {
                accessToken = token;
            }
        }
    }
}