using System.Text;
using Kontur.Extern.Client.Http.Constants;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeClusterClientSetup
    {
        private readonly FakeHttpMessages httpMessages;

        public FakeClusterClientSetup(FakeHttpMessages httpMessages) => 
            this.httpMessages = httpMessages;

        public FakeClusterClientSetup SetJsonResponse(ResponseCode responseCode, string responseBody)
        {
            var headers = new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json);
            var expectedResponse = new Response(responseCode, new Content(Encoding.UTF8.GetBytes(responseBody)), headers);
            httpMessages.SetExpectedResponse(expectedResponse);
            return this;
        }

        public void SetResponseBody(byte[] body) => httpMessages.ReplaceResponseBody(body);

        public void SetResponseCode(ResponseCode responseCode) => httpMessages.ReplaceResponseCode(responseCode);
    }
}