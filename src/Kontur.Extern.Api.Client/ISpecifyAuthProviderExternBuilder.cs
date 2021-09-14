using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Auth.OpenId.Builder;

namespace Kontur.Extern.Api.Client
{
    public delegate OpenIdAuthenticationProviderBuilder.Configured OpenIdSetup(OpenIdAuthenticationProviderBuilder builder);
    
    [PublicAPI]
    public interface ISpecifyAuthProviderExternBuilder
    {
        IExternBuilder WithOpenIdAuthProvider(OpenIdSetup setup);
    }
}