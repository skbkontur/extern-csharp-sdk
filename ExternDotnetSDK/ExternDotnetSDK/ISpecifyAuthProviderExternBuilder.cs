using JetBrains.Annotations;
using Kontur.Extern.Client.Auth.OpenId.Builder;

namespace Kontur.Extern.Client
{
    public delegate OpenIdAuthenticationProviderBuilder.Configured OpenIdSetup(OpenIdAuthenticationProviderBuilder builder);
    
    [PublicAPI]
    public interface ISpecifyAuthProviderExternBuilder
    {
        IExternBuilder WithOpenIdAuthProvider(OpenIdSetup setup);
    }
}