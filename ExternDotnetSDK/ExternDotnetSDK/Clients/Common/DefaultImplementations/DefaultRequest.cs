using System;
using System.Net.Http;
using System.Net.Http.Headers;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;

namespace ExternDotnetSDK.Clients.Common.DefaultImplementations
{
    public class DefaultRequest : IHaveHttpRequestMessage
    {
        private readonly HttpRequestMessage requestMessage;

        public DefaultRequest(HttpRequestMessage message) => requestMessage = message;
        public HttpRequestHeaders Headers => requestMessage.Headers;
        public HttpContent Content => requestMessage.Content;
        public HttpMethod Method => requestMessage.Method;
        public Uri Uri => requestMessage.RequestUri;
    }
}