using JetBrains.Annotations;
using Kontur.Extern.Client.Authentication.OpenId.Builder;

namespace Kontur.Extern.Client
{
    public delegate IOpenIdAuthenticationProviderBuilder OpenIdSetup(ISpecifyClusterClientOpenIdAuthenticationProviderBuilder builder);
    
    [PublicAPI]
    public interface ISpecifyAuthProviderExternFactory
    {
        IExternFactory WithOpenIdAuthProvider(OpenIdSetup setup);
    }
}