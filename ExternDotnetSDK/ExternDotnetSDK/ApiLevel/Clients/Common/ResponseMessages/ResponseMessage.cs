using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Common.RequestMessages;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Common.ResponseMessages
{
    public class ResponseMessage : IResponseMessage
    {
        private readonly HttpResponseMessage responseMessage;

        public ResponseMessage(HttpResponseMessage message) => responseMessage = message;

        public HttpContent Content => responseMessage.Content;
        public Dictionary<string, string> Headers
        {
            get
            {
                var result = new Dictionary<string, string>();
                foreach (var header in responseMessage.Headers)
                foreach (var value in header.Value)
                    result.Add(header.Key, value);
                return result;
            }
        }
        public HttpStatusCode StatusCode => responseMessage.StatusCode;

        public IRequestMessage Request => new RequestMessage(responseMessage.RequestMessage);
        public string ReasonPhrase => responseMessage.ReasonPhrase;

        public IResponseMessage EnsureSuccessStatusCode()
        {
            responseMessage.EnsureSuccessStatusCode();
            return this;
        }
    }
}