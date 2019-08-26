using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ExternDotnetSDK.Clients.Common.RequestMessages
{
    public class RequestMessage : IRequestMessage
    {
        private readonly HttpRequestMessage requestMessage;

        public RequestMessage(HttpRequestMessage message) => requestMessage = message;
        public HttpRequestHeaders Headers => requestMessage.Headers;
        public HttpContent Content => requestMessage.Content;
        public HttpMethod Method => requestMessage.Method;
        public Uri Uri => requestMessage.RequestUri;
    }
}