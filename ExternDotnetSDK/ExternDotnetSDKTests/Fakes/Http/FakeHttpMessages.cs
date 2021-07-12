#nullable enable
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Tests.Fakes.Http
{
    internal class FakeHttpMessages
    {
        public FakeHttpMessages(Response expectedResponse) => ExpectedResponse = expectedResponse;

        public Request? SentRequest { get; set; }
        public Response ExpectedResponse { get; }
    }
}