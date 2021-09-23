using Kontur.Extern.Api.Client.Auth.OpenId.Builder;

namespace Kontur.Extern.Api.Client
{
    public delegate OpenIdAuthenticatorBuilder.Configured OpenIdSetup(OpenIdAuthenticatorBuilder builder);
}