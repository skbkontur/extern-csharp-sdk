#nullable enable
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models
{
    [PublicAPI]
    public interface IOpenIdAuthenticationContext
    {
        bool TryGetAccessToken(out IAccessToken token);
        void SetAccessToken(IAccessToken token);
    }
}