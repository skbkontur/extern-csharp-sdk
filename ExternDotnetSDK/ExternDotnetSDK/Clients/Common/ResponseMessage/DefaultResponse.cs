using System.Net.Http;

namespace ExternDotnetSDK.Clients.Common.ResponseMessage
{
    public class DefaultResponse : IHaveHttpResponseMessage
    {
        public DefaultResponse(HttpResponseMessage httpResponseMessage) => HttpResponseMessage = httpResponseMessage;

        public HttpResponseMessage HttpResponseMessage { get; }
    }
}