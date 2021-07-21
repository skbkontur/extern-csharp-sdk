using JetBrains.Annotations;

namespace Kontur.Extern.Client.Authentication.OpenId.Builder
{
    [PublicAPI]
    public interface ISpecifyClientIdOpenIdAuthenticationProviderBuilder
    {
        ISpecifyAuthStrategyOpenIdAuthenticationProviderBuilder WithClientIdentification(string clientId, string apiKey);
    }
}