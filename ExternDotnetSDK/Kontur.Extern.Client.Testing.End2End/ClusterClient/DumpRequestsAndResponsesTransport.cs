using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Testing.End2End.ClusterClient
{
    public class DumpRequestsAndResponsesTransport : ITransport
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
            var (requestText, recreatedRequest) = DumpRequest(request);
            log.Debug($"Request: {requestText}");

            var response = await transport.SendAsync(recreatedRequest, connectionTimeout, timeout, cancellationToken);

            log.Debug($"Response: {DumpResponse(response)}");

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

            static (string requestText, Request recreatedRequest) DumpRequest(Request request)
            {
                var recreatedRequest = request;
                var requestDump = new StringBuilder();
                requestDump.AppendLine(request.ToString(true, true));
                if (request.Headers?.ContentType?.StartsWith("application/octet-stream") != true)
                {
                    if (request.Content != null)
                    {
                        requestDump.AppendLine(request.Content.ToString());
                    }
                    else if (request.StreamContent?.Stream is MemoryStream memoryStream)
                    {
                        requestDump.AppendLine(DumpStreamAndRewind(memoryStream));
                        requestDump.AppendLine();

                        recreatedRequest = new Request(request.Method, request.Url, new StreamContent(memoryStream, request.StreamContent.Length), request.Headers);
                    }
                }

                return (requestDump.ToString(), recreatedRequest);
            }
        }

        private static string DumpStreamAndRewind(MemoryStream memoryStream)
        {
            var position = memoryStream.Position;
            try
            {
                using var streamReader = new StreamReader(memoryStream, leaveOpen: true);
                return streamReader.ReadToEnd();
            }
            finally
            {
                memoryStream.Position = position;
            }
        }

        public TransportCapabilities Capabilities => transport.Capabilities;
    }
}