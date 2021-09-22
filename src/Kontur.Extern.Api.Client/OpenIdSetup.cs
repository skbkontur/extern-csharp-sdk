using Kontur.Extern.Api.Client.Auth.OpenId.Builder;

namespace Kontur.Extern.Api.Client
{
    public delegate OpenIdAuthenticationProviderBuilder.Configured OpenIdSetup(OpenIdAuthenticationProviderBuilder builder);
}