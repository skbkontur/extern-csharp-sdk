#nullable enable
using System;
using System.IO;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Clients.Common.Requests
{
    // todo: when Request will no longer need, remove this alias
    using CCRequest=Vostok.Clusterclient.Core.Model.Request;

    public readonly struct RequestBuilder
    {
        public static RequestBuilder Get(string url) => Get(new Uri(url, UriKind.RelativeOrAbsolute));
        public static RequestBuilder Get(Uri url) => new(CCRequest.Get(url));

        
        public static RequestBuilder Post(string url) => Post(new Uri(url, UriKind.RelativeOrAbsolute));
        public static RequestBuilder Post(Uri url) => new(CCRequest.Post(url));

        
        public static RequestBuilder Put(string url) => Put(new Uri(url, UriKind.RelativeOrAbsolute));
        public static RequestBuilder Put(Uri url) => new(CCRequest.Put(url));

        public static RequestBuilder Delete(string url) => Delete(new Uri(url, UriKind.RelativeOrAbsolute));
        public static RequestBuilder Delete(Uri url) => new(CCRequest.Delete(url));
        
        private readonly CCRequest request;

        private RequestBuilder(CCRequest request) => this.request = request;

        public bool IsWriteRequest => !request.Method.Equals(RequestMethods.Get, StringComparison.OrdinalIgnoreCase);

        public RequestBuilder WithUrl(string url) => WithUrl(new Uri(url, UriKind.RelativeOrAbsolute));

        public RequestBuilder WithUrl(Uri url) => new(request.WithUrl(url));

        public RequestBuilder WithContent(byte[] content) => new(request.WithContent(content));

        public RequestBuilder WithContent(Stream stream) => new(request.WithContent(stream));

        public RequestBuilder WithJson(string jsonContent) => new(request.WithContent(jsonContent).WithContentTypeHeader(SenderConstants.MediaType));

        public override string ToString() => request.ToString(true, false);

        internal CCRequest BuildRequest(string apiKey, string sessionId, TimeSpan? timeout)
        {
            var resultRequest = request
                .WithAuthorizationHeader(SenderConstants.AuthSidHeader, sessionId)
                .WithHeader(SenderConstants.ApiKeyHeader, apiKey);

            return timeout == null 
                ? resultRequest 
                : resultRequest.WithHeader(SenderConstants.TimeoutHeader, timeout.Value.ToString("c"));
        }
    }
}