using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.Authentication.OpenId.Builder;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public interface ISpecifyAuthProviderExternFactory
    {
        IExternFactory WithOpenIdAuthProvider(Func<ISpecifyClientIdOpenIdAuthenticationProviderBuilder, IOpenIdAuthenticationProviderBuilder> openIdBuilder);
    }
}