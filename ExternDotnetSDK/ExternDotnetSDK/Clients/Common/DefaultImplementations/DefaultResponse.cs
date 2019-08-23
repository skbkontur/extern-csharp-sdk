using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;

namespace ExternDotnetSDK.Clients.Common.DefaultImplementations
{
    public class DefaultResponse : IHaveHttpResponseMessage
    {
        private readonly HttpResponseMessage responseMessage;

        public DefaultResponse(HttpResponseMessage message) => responseMessage = message;

        public HttpContent Content => responseMessage.Content;
        public HttpResponseHeaders Headers => responseMessage.Headers;
        public HttpStatusCode StatusCode => responseMessage.StatusCode;

        public IHaveHttpRequestMessage Request => new DefaultRequest(responseMessage.RequestMessage);
        public string ReasonPhrase => responseMessage.ReasonPhrase;

        public IHaveHttpResponseMessage EnsureSuccessStatusCode()
        {
            responseMessage.EnsureSuccessStatusCode();
            return this;
        }
    }
}