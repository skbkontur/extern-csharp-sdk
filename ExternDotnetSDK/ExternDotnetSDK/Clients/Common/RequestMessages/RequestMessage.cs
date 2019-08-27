using System;
using System.Collections.Generic;
using System.Net.Http;

namespace KeApiOpenSdk.Clients.Common.RequestMessages
{
    public class RequestMessage : IRequestMessage
    {
        private readonly HttpRequestMessage requestMessage;

        public RequestMessage(HttpRequestMessage message) => requestMessage = message;
        public Dictionary<string, string> Headers
        {
            get
            {
                var result = new Dictionary<string, string>();
                foreach (var header in requestMessage.Headers)
                foreach (var value in header.Value)
                    result.Add(header.Key, value);
                return result;
            }
        }
        public HttpContent Content => requestMessage.Content;
        public HttpMethod Method => requestMessage.Method;
        public Uri Uri => requestMessage.RequestUri;
    }
}