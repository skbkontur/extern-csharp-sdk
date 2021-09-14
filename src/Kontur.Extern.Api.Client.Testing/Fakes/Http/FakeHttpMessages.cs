using System;
using System.Collections.Generic;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Testing.Fakes.Http
{
    public class FakeHttpMessages
    {
        private readonly List<Request> sentRequests;
        private Response? expectedResponse;

        public FakeHttpMessages() => 
            sentRequests = new List<Request>();

        public Response ExpectedResponse
        {
            get => expectedResponse ?? throw new ArgumentNullException(nameof(expectedResponse));
            private set => expectedResponse = value;
        }
        
        public IEnumerable<Request> SentRequests => sentRequests;

        public void ReplaceResponseBody(byte[] body) => ExpectedResponse = new Response(ExpectedResponse.Code, new Content(body), ExpectedResponse.Headers);

        public void ReplaceResponseCode(ResponseCode responseCode) => ExpectedResponse = new Response(responseCode, ExpectedResponse.Content, ExpectedResponse.Headers);

        public void TheRequestWasSent(Request request) => sentRequests.Add(request);

        public void SetExpectedResponse(Response response) => 
            ExpectedResponse = response;
    }
}