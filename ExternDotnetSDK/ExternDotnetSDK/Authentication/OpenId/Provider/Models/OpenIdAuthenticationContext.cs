#nullable enable
namespace Kontur.Extern.Client.Authentication.OpenId.Provider.Models
{
    internal class OpenIdAuthenticationContext
    {
        private readonly object syncObject = new();
        private AccessToken? accessToken;

        public bool TryGetAccessToken(out AccessToken token)
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

        public void SetAccessToken(AccessToken token)
        {
            lock (syncObject)
            {
                accessToken = token;
            }
        }
    }
}