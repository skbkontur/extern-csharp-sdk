using System.Net.Http;
using System.Threading;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Contents;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Header;
using Vostok.Clusterclient.Core.Model;
using StreamContent = Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Contents.StreamContent;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Messages
{
    internal static class RequestMessageFactory
    {
        private static readonly HttpMethod PatchMethod = new HttpMethod(RequestMethods.Patch);

        public static HttpRequestMessage Create(Request request, CancellationToken token)
        {
            var message = new HttpRequestMessage(TranslateMethod(request.Method), request.Url)
            {
                Content = TranslateContent(request, token)
            };

            RequestHeadersConverter.Fill(request, message);

            return message;
        }

        private static HttpMethod TranslateMethod(string method)
        {
            switch (method)
            {
                case RequestMethods.Get:
                    return HttpMethod.Get;
                case RequestMethods.Post:
                    return HttpMethod.Post;
                case RequestMethods.Put:
                    return HttpMethod.Put;
                case RequestMethods.Patch:
                    return PatchMethod;
                case RequestMethods.Delete:
                    return HttpMethod.Delete;
                case RequestMethods.Head:
                    return HttpMethod.Head;
                case RequestMethods.Options:
                    return HttpMethod.Options;
                case RequestMethods.Trace:
                    return HttpMethod.Trace;
                default:
                    return new HttpMethod(method);
            }
        }

        private static HttpContent? TranslateContent(Request request, CancellationToken cancellationToken)
        {
            if (request.Content != null && request.Content.Length > 0)
                return new BufferContent(request.Content, cancellationToken);

            if (request.CompositeContent != null && request.CompositeContent.Length > 0)
                return new CompositeBufferContent(request.CompositeContent, cancellationToken);

            if (request.StreamContent != null)
                return new StreamContent(request.StreamContent, cancellationToken);

            return null;
        }
    }
}
