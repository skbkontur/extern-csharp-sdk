using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Strategies;
using Vostok.Clusterclient.Core.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeClusterClient : ClusterClient
    {
        private readonly FakeHttpMessages httpMessages;

        public FakeClusterClient(string baseUrl, FakeHttpMessages httpMessages, ILog log)
            : base(
                log,
                cfg =>
                {
                    cfg.SetupExternalUrl(new Uri(baseUrl));
                    cfg.Transport = new FakeTransport(httpMessages);
                    cfg.DefaultRequestStrategy = new SingleReplicaRequestStrategy();
                })
        {
            this.httpMessages = httpMessages;
        }

        private class FakeTransport : ITransport
        {
            private readonly FakeHttpMessages httpMessages;

            public FakeTransport(FakeHttpMessages httpMessages)
            {
                this.httpMessages = httpMessages;
            }

            public TransportCapabilities Capabilities { get; } = TransportCapabilities.RequestStreaming |
                                                                 TransportCapabilities.ResponseStreaming |
                                                                 TransportCapabilities.RequestCompositeBody;

            public Task<Response> SendAsync(Request request, TimeSpan? connectionTimeout, TimeSpan timeout, CancellationToken cancellationToken)
            {
                httpMessages.TheRequestWasSent(request);
                return Task.FromResult(httpMessages.ExpectedResponse);
            }
        }
    }
}