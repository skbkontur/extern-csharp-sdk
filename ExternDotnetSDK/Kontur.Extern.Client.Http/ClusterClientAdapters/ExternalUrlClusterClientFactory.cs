using System;
using Kontur.Extern.Client.Http.Exceptions;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Http.ClusterClientAdapters
{
    public class ExternalUrlClusterClientFactory : IClusterClientFactory
    {
        private readonly Uri externalUrl;

        public ExternalUrlClusterClientFactory(Uri externalUrl)
        {
            if (externalUrl == null)
                throw new ArgumentNullException(nameof(externalUrl));
            if (!externalUrl.IsAbsoluteUri)
                throw Errors.UrlShouldBeAbsolute(nameof(externalUrl), externalUrl);
            
            this.externalUrl = externalUrl;
        }

        public IClusterClient Create(ILog log)
        {
            return new ClusterClient(
                log,
                cfg =>
                {
                    cfg.SetupUniversalTransport();
                    cfg.SetupExternalUrl(externalUrl);
                    cfg.MaxReplicasUsedPerRequest = 1;
                    
                    cfg.Logging.LogReplicaRequests = false;
                    cfg.Logging.LogResultDetails = false;
                });
        }
    }
}