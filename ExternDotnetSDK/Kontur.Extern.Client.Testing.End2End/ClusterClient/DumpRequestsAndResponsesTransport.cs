using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kontur.Extern.Client.Http.Constants;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Testing.End2End.ClusterClient
{
    public class DumpRequestsAndResponsesTransport : ITransport
    {
        private const int LimitDumpContent = 4096;
        private const string TooBigContentPlaceholder = "<content>";
        private readonly ITransport transport;
        private readonly ILog log;

        public DumpRequestsAndResponsesTransport(ITransport transport, ILog log)
        {
            this.transport = transport;
            this.log = log;
        }
        
        public TransportCapabilities Capabilities => transport.Capabilities;

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
                if (response.HasContent)
                {
                    if (response.Headers.ContentType?.StartsWith(ContentTypes.Json) == true)
                    {
                        DumpContent(responseDump, response.Content);
                    }
                    else
                    {
                        responseDump.AppendLine(TooBigContentPlaceholder);
                    }
                }

                return responseDump.ToString();
            }

            static (string requestText, Request recreatedRequest) DumpRequest(Request request)
            {
                var recreatedRequest = request;
                var requestDump = new StringBuilder();
                requestDump.AppendLine(request.ToString(true, true));
                if (recreatedRequest.Headers?.ContentType?.StartsWith(ContentTypes.Json) == true)
                {
                    if (recreatedRequest.Content != null)
                    {
                        DumpContent(requestDump, recreatedRequest.Content);
                    }
                    else if (recreatedRequest.StreamContent != null)
                    {
                        recreatedRequest = DumpRequestStream(requestDump, recreatedRequest);
                    }
                }
                else if (recreatedRequest.HasBody)
                {
                    requestDump.AppendLine(TooBigContentPlaceholder);
                }

                return (requestDump.ToString(), recreatedRequest);
            }
        }

        private static Request DumpRequestStream(StringBuilder dump, Request request)
        {
            if (request.StreamContent == null)
                return request;
            
            if (request.StreamContent.Length < LimitDumpContent)
            {
                dump.AppendLine(TooBigContentPlaceholder);
                return request;
            }

            var stream = request.StreamContent.Stream;
            if (stream is MemoryStream {Length: < LimitDumpContent} memoryStream)
            {
                dump.AppendLine(DumpStreamAndRewind(memoryStream));
                dump.AppendLine();
            }
            else
            {
                dump.AppendLine(TooBigContentPlaceholder);
            }

            return new Request(request.Method, request.Url, new StreamContent(stream, request.StreamContent.Length), request.Headers);
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
        
        private static void DumpContent(StringBuilder dump, Content content) => 
            dump.AppendLine(content.Length > LimitDumpContent ? TooBigContentPlaceholder : content.ToString());
    }
}