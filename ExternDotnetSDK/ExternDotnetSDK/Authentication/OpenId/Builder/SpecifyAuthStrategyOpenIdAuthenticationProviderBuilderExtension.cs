using System;

namespace Kontur.Extern.Client.Authentication.OpenId.Builder
{
    public static class SpecifyAuthStrategyOpenIdAuthenticationProviderBuilderExtension
    {
        public static IOpenIdAuthenticationProviderBuilder WithAuthenticationStrategy(
            this ISpecifyAuthStrategyOpenIdAuthenticationProviderBuilder builder,
            Func<ISpecifyAuthStrategyOpenIdAuthenticationProviderBuilder, IOpenIdAuthenticationProviderBuilder> setupStrategy)
        {
            return setupStrategy(builder);
        }
    }
}