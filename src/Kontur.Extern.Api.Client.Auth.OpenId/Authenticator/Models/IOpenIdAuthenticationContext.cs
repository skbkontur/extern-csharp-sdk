#nullable enable
namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models
{
    public interface IOpenIdAuthenticationContext
    {
        bool TryGetAccessToken(out IAccessToken token);
        void SetAccessToken(IAccessToken token);
    }
}