using JetBrains.Annotations;

namespace Kontur.Extern.Client.Authentication.OpenId.Builder
{
    [PublicAPI]
    public interface ISpecifyAuthStrategyOpenIdAuthenticationProviderBuilder : IBasicClientOpenIdAuthenticationProviderBuilder
    {
        IOpenIdAuthenticationProviderBuilder WithAuthenticationByPassword(string username, string password);
    }
}