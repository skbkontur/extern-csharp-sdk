using System.Net.Http;
using System.Net.Http.Headers;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Header
{
    internal static class RequestHeadersConverter
    {
        public static void Fill(Request request, HttpRequestMessage message)
        {
            if (request.Headers == null)
                return;
            AssignHeaders(request.Headers, message);
            TrySetHostExplicitly(request.Headers, message.Headers);
        }

        private static void AssignHeaders(Headers headers, HttpRequestMessage message)
        {
            foreach (var header in headers)
            {
                if (NeedToSkipHeader(header.Name))
                    continue;

                if (IsContentHeader(header.Name))
                {
                    message.Content.Headers.Add(header.Name, header.Value);
                    continue;
                }

                message.Headers.Add(header.Name, header.Value);
            }
        }

        private static bool NeedToSkipHeader(string name) => name.Equals(HeaderNames.Connection) ||
                                                             name.Equals(HeaderNames.ContentLength) ||
                                                             name.Equals(HeaderNames.Host) ||
                                                             name.Equals(HeaderNames.TransferEncoding);

        private static bool IsContentHeader(string headerName)
        {
            switch (headerName)
            {
                case HeaderNames.Allow:
                case HeaderNames.ContentDisposition:
                case HeaderNames.ContentEncoding:
                case HeaderNames.ContentLanguage:
                case HeaderNames.ContentLength:
                case HeaderNames.ContentLocation:
                case HeaderNames.ContentMD5:
                case HeaderNames.ContentRange:
                case HeaderNames.ContentType:
                case HeaderNames.Expires:
                case HeaderNames.LastModified:
                    return true;

                default:
                    return false;
            }
        }

        private static void TrySetHostExplicitly(Headers source, HttpRequestHeaders target)
        {
            var host = source[HeaderNames.Host];
            if (host != null)
                target.Host = host;
        }
    }
}
