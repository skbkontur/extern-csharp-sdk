using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.IntegrationTests.TestClusterClient
{
    internal class DumpRequestsAndResponsesTransport : ITransport
    {
        private readonly ITransport transport;
        private readonly ILog log;

        public DumpRequestsAndResponsesTransport(ITransport transport, ILog log)
        {
            this.transport = transport;
            this.log = log;
        }

        public async Task<Response> SendAsync(Request request, TimeSpan? connectionTimeout, TimeSpan timeout, CancellationToken cancellationToken)
        {
            log.Debug(DumpRequest(request));

            var response = await transport.SendAsync(request, connectionTimeout, timeout, cancellationToken);

            log.Debug(DumpResponse(response));

            return response;

            static string DumpResponse(Response response)
            {
                var responseDump = new StringBuilder();
                responseDump.AppendLine(response.ToString(true));
                if (response.HasContent && response.Headers.ContentType?.StartsWith("application/octet-stream") != true)
                {
                    responseDump.AppendLine(response.Content.ToString());
                }

                return responseDump.ToString();
            }

            static string DumpRequest(Request request)
            {
                var requestDump = new StringBuilder();
                requestDump.AppendLine(request.ToString(true, true));
                if (request.Content != null && request.Headers?.ContentType?.StartsWith("application/octet-stream") != true)
                {
                    requestDump.AppendLine(request.Content.ToString());
                }

                return requestDump.ToString();
            }
        }

        public TransportCapabilities Capabilities => transport.Capabilities;
    }
}