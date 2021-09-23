using System;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Builder
{
    public static class SpecifyAuthStrategyOpenIdAuthenticatorBuilderExtension
    {
        public static OpenIdAuthenticatorBuilder.Configured WithAuthenticationStrategy(
            this OpenIdAuthenticatorBuilder.SpecifyAuthStrategy builder,
            Func<OpenIdAuthenticatorBuilder.SpecifyAuthStrategy, OpenIdAuthenticatorBuilder.Configured> setupStrategy)
        {
            return setupStrategy(builder);
        }
    }
}