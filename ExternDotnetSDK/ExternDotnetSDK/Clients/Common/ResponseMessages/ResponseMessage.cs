using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using KeApiOpenSdk.Clients.Common.RequestMessages;

namespace KeApiOpenSdk.Clients.Common.ResponseMessages
{
    public class ResponseMessage : IResponseMessage
    {
        private readonly HttpResponseMessage responseMessage;

        public ResponseMessage(HttpResponseMessage message) => responseMessage = message;

        public HttpContent Content => responseMessage.Content;
        public HttpResponseHeaders Headers => responseMessage.Headers;
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