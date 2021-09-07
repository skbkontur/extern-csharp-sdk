using System;
using Kontur.Extern.Client.Http.Exceptions;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;

namespace Kontur.Extern.Client.Http.Configurations
{
    public class ExternalUrlHttpClientConfiguration : IHttpClientConfiguration
    {
        private readonly Uri externalUrl;

        public ExternalUrlHttpClientConfiguration(Uri externalUrl)
        {
            if (externalUrl == null)
                throw new ArgumentNullException(nameof(externalUrl));
            if (!externalUrl.IsAbsoluteUri)
                throw Errors.UrlShouldBeAbsolute(nameof(externalUrl), externalUrl);
            
            this.externalUrl = externalUrl;
        }
        
        public void Apply(IClusterClientConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            
            config.SetupUniversalTransport();
            config.SetupExternalUrl(externalUrl);
            config.MaxReplicasUsedPerRequest = 1;
        }
    }
}