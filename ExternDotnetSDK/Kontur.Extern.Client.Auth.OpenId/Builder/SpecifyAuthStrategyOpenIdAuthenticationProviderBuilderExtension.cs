using System;

namespace Kontur.Extern.Client.Auth.OpenId.Builder
{
    public static class SpecifyAuthStrategyOpenIdAuthenticationProviderBuilderExtension
    {
        public static OpenIdAuthenticationProviderBuilder.Configured WithAuthenticationStrategy(
            this OpenIdAuthenticationProviderBuilder.SpecifyAuthStrategy builder,
            Func<OpenIdAuthenticationProviderBuilder.SpecifyAuthStrategy, OpenIdAuthenticationProviderBuilder.Configured> setupStrategy)
        {
            return setupStrategy(builder);
        }
    }
}