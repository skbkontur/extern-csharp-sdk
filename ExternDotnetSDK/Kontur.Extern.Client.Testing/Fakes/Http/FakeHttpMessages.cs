#nullable enable
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeHttpMessages
    {
        public FakeHttpMessages(Response expectedResponse) => ExpectedResponse = expectedResponse;

        public Request? SentRequest { get; set; }
        public Response ExpectedResponse { get; private set; }

        public void ReplaceResponseBody(byte[] body) => ExpectedResponse = new Response(ExpectedResponse.Code, new Content(body), ExpectedResponse.Headers);

        public void ReplaceResponseCode(ResponseCode responseCode) => ExpectedResponse = new Response(responseCode, ExpectedResponse.Content, ExpectedResponse.Headers);
    }
}