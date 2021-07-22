#nullable enable
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kontur.Extern.Client.Tests.Fakes.Logging;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Strategies;
using Vostok.Clusterclient.Core.Transport;

namespace Kontur.Extern.Client.Tests.Fakes.Http
{
    internal class FakeClusterClient : ClusterClient
    {
        private readonly FakeHttpMessages httpMessages;

        public FakeClusterClient(string baseUrl, FakeHttpMessages httpMessages)
            : base(
                new TestLog(),
                cfg =>
                {
                    cfg.SetupExternalUrl(new Uri(baseUrl));
                    cfg.Transport = new FakeTransport(httpMessages);
                    cfg.DefaultRequestStrategy = new SingleReplicaRequestStrategy();
                })
        {
            BaseUrl = baseUrl;
            this.httpMessages = httpMessages;
        }

        public string BaseUrl { get; }
        public Request? SentRequest => httpMessages.SentRequest;
        public string? SentContentString
        {
            get
            {
                var request = httpMessages.SentRequest;
                if (request == null)
                    return null;

                if (request.Content != null)
                    return request.Content.ToString();

                if (request.StreamContent != null)
                {
                    var memoryStream = (MemoryStream) request.StreamContent.Stream;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }

                return null;
            }
        }

        public void SetResponseBody(byte[] body) => httpMessages.ReplaceResponseBody(body);

        public void SetResponseCode(ResponseCode responseCode) => httpMessages.ReplaceResponseCode(responseCode);

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
                httpMessages.SentRequest = request;
                return Task.FromResult(httpMessages.ExpectedResponse);
            }
        }
    }
}