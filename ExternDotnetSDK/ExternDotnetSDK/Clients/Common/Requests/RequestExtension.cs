using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Kontur.Extern.Client.Clients.Common.RequestSenders;

namespace Kontur.Extern.Client.Clients.Common.Requests
{
    static class RequestExtension
    {
        public static HttpRequestMessage ToHttpRequestMessage(this Request request)
        {
            var httpRequestMessage = new HttpRequestMessage(ConvertHttpMethod(request.Method), request.Url);
            AddJsonContent(httpRequestMessage, request);
            AddHeaders(httpRequestMessage, request);
            return httpRequestMessage;
        }

        private static void AddHeaders(HttpRequestMessage httpRequestMessage, Request request)
        {
            if (request.Headers != null)
            {
                foreach (var header in request.Headers)
                {
                    httpRequestMessage.Headers.Add(header.Key, header.Value);
                }
            }
        }

        private static void AddJsonContent(HttpRequestMessage httpRequestMessage, Request request)
        {
            if (request.Method == RequestMethod.Get || string.IsNullOrEmpty(request.JsonContent))
                return;
            httpRequestMessage.Content = new StringContent(request.JsonContent);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(SenderConstants.MediaType);
        }

        private static HttpMethod ConvertHttpMethod(RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }
        }
    }
}