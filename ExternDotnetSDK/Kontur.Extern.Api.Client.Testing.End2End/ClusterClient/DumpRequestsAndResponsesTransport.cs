using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Http.Constants;
using Kontur.Extern.Api.Client.Testing.Helpers.Json;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.Testing.End2End.ClusterClient
{
    public class DumpRequestsAndResponsesTransport : ITransport
    {
        private const int LimitDumpRequestContent = 4096; // 4 Kb
        private const int LimitDumpResponseContent = 1048576; // 1 Mb
        private const string PayloadStub = "<content>";
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
                        DumpJsonContent(responseDump, response.Content, LimitDumpResponseContent);
                    }
                    else
                    {
                        responseDump.AppendLine(PayloadStub);
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
                        DumpJsonContent(requestDump, recreatedRequest.Content, LimitDumpRequestContent);
                    }
                    else if (recreatedRequest.StreamContent != null)
                    {
                        recreatedRequest = DumpRequestStream(requestDump, recreatedRequest);
                    }
                }
                else if (recreatedRequest.HasBody)
                {
                    requestDump.AppendLine(PayloadStub);
                }

                return (requestDump.ToString(), recreatedRequest);
            }
        }

        private static Request DumpRequestStream(StringBuilder dump, Request request)
        {
            if (request.StreamContent == null)
                return request;
            
            if (request.StreamContent.Length < LimitDumpRequestContent)
            {
                dump.AppendLine(PayloadStub);
                return request;
            }

            var stream = request.StreamContent.Stream;
            if (stream is MemoryStream {Length: < LimitDumpRequestContent} memoryStream)
            {
                dump.AppendLine(DumpJsonStreamAndRewind(memoryStream));
                dump.AppendLine();
            }
            else
            {
                dump.AppendLine(PayloadStub);
            }

            return new Request(request.Method, request.Url, new StreamContent(stream, request.StreamContent.Length), request.Headers);
        }

        private static string DumpJsonStreamAndRewind(MemoryStream memoryStream)
        {
            var position = memoryStream.Position;
            try
            {
                using var streamReader = new StreamReader(memoryStream, leaveOpen: true);
                var json = streamReader.ReadToEnd();
                return json.EllipsisLongStringValuesInJson();
            }
            finally
            {
                memoryStream.Position = position;
            }
        }
        
        private static void DumpJsonContent(StringBuilder dump, Content content, int limit) => 
            dump.AppendLine(content.Length > limit ? PayloadStub : content.ToString().EllipsisLongStringValuesInJson());
    }
}