using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.Authentication.OpenId.Time;

namespace Kontur.Extern.Client.Authentication.OpenId.Builder
{
    [PublicAPI]
    public interface IOpenIdAuthenticationProviderBuilder : IBasicClientOpenIdAuthenticationProviderBuilder
    {
        IAuthenticationProvider Build();

        IOpenIdAuthenticationProviderBuilder SubstituteStopwatch(IStopwatchFactory stopwatchFactory);
        IOpenIdAuthenticationProviderBuilder RefreshAccessTokensBeforeExpirationsProactivelyWithinInterval(TimeSpan interval);
    }
}