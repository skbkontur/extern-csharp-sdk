using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Transport;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    internal class FakeTransport : ITransport
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