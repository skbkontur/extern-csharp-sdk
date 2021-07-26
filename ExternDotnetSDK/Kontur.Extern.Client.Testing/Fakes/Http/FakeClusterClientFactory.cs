#nullable enable
using System;
using System.Text;
using Kontur.Extern.Client.Http.Constants;
using Vostok.Clusterclient.Core.Model;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeClusterClientFactory
    {
        public static FakeClusterClientFactory WithDefaultBaseUrl() => WithBaseUrl("https://test/");
        public static FakeClusterClientFactory WithBaseUrl(string baseUrl) => new(baseUrl);
        
        private readonly string baseUrl;
        private Response? expectedResponse;

        private FakeClusterClientFactory(string baseUrl) => this.baseUrl = baseUrl;

        public FakeClusterClientFactory WithJsonResponse(ResponseCode responseCode, string responseBody)
        {
            var headers = new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json);
            expectedResponse = new Response(responseCode, new Content(Encoding.UTF8.GetBytes(responseBody)), headers);
            return this;
        }
        
        public FakeClusterClient CreateFakeClusterClient(ILog log)
        {
            if (expectedResponse == null)
                throw new ArgumentNullException(nameof(expectedResponse));

            return new(baseUrl, new FakeHttpMessages(expectedResponse), log);
        }
    }
}