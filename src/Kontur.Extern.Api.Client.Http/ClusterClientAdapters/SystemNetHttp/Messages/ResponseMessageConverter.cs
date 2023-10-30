using System.IO;
using System.Net.Http;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Header;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Messages
{
    internal static class ResponseMessageConverter
    {
        public static Response Convert(
            HttpResponseMessage message,
            Content? content,
            Stream? stream)
        {
            var code = (ResponseCode) (int) message.StatusCode;

            var headers = ResponseHeadersConverter.Convert(message);

            return new Response(code, content, headers, stream);
        }
    }
}