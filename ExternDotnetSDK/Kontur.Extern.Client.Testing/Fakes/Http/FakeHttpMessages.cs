using System.Collections.Generic;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeHttpMessages
    {
        private readonly List<Request> sentRequests;

        public FakeHttpMessages(Response expectedResponse)
        {
            ExpectedResponse = expectedResponse;
            sentRequests = new List<Request>();
        }

        public Response ExpectedResponse { get; private set; }
        public IEnumerable<Request> SentRequests => sentRequests;

        public void ReplaceResponseBody(byte[] body) => ExpectedResponse = new Response(ExpectedResponse.Code, new Content(body), ExpectedResponse.Headers);

        public void ReplaceResponseCode(ResponseCode responseCode) => ExpectedResponse = new Response(responseCode, ExpectedResponse.Content, ExpectedResponse.Headers);

        public void TheRequestWasSent(Request request) => sentRequests.Add(request);
    }
}