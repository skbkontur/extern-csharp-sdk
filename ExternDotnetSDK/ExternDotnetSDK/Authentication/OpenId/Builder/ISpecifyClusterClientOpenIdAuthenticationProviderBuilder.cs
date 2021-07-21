#nullable enable
using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.HttpLevel.Options;
using Vostok.Clusterclient.Core;

namespace Kontur.Extern.Client.Authentication.OpenId.Builder
{
    [PublicAPI]
    public interface ISpecifyClusterClientOpenIdAuthenticationProviderBuilder : IBasicClientOpenIdAuthenticationProviderBuilder
    {
        ISpecifyClientIdOpenIdAuthenticationProviderBuilder WithExternApiUrl(Uri url, RequestSendingOptions? options = null);

        ISpecifyClientIdOpenIdAuthenticationProviderBuilder WithClusterClient(IClusterClient clusterClient, RequestSendingOptions? options = null);
    }
}