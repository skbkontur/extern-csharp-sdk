using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Kontur.Extern.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Client.Http.Constants;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeClusterClientFactory : IClusterClientFactory
    {
        public static FakeClusterClientFactory WithDefaultBaseUrl() => WithBaseUrl("https://test/");
        public static FakeClusterClientFactory WithBaseUrl(string baseUrl) => new(baseUrl);

        private Response? expectedResponse;
        private readonly FakeHttpMessages httpMessages;

        private FakeClusterClientFactory(string baseUrl)
        {
            BaseUrl = baseUrl;
            httpMessages = new FakeHttpMessages();
            Setup = new FakeClusterClientSetup(httpMessages);
            Verify = new FakeClusterClientVerify(httpMessages);
        }
        
        public string BaseUrl { get; }

        public FakeClusterClientFactory WithJsonResponse(ResponseCode responseCode, string responseBody)
        {
            var headers = new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json);
            expectedResponse = new Response(responseCode, new Content(Encoding.UTF8.GetBytes(responseBody)), headers);
            httpMessages.SetExpectedResponse(expectedResponse);
            return this;
        }

        public IClusterClient Create(ILog log)
        {
            if (expectedResponse == null)
                throw new ArgumentNullException(nameof(expectedResponse));

            return new FakeClusterClient(BaseUrl, httpMessages, log);
        }

        public FakeClusterClientSetup Setup { get; }
        public FakeClusterClientVerify Verify { get; }

        public class FakeClusterClientVerify
        {
            private readonly FakeHttpMessages httpMessages;

            public FakeClusterClientVerify(FakeHttpMessages httpMessages) => 
                this.httpMessages = httpMessages;
            
            public string? SentContentString
            {
                get
                {
                    var request = SentRequest;
                    if (request == null)
                        return null;

                    if (request.Content != null)
                        return request.Content.ToString();

                    if (request.StreamContent != null)
                    {
                        var memoryStream = (MemoryStream) request.StreamContent.Stream;
                        return Encoding.UTF8.GetString(memoryStream.ToArray());
                    }

                    return null;
                }
            }
            
            public Request? SentRequest => httpMessages.SentRequests.LastOrDefault();
            public IEnumerable<Request> SentRequests => httpMessages.SentRequests;
        }

        public class FakeClusterClientSetup
        {
            private readonly FakeHttpMessages httpMessages;

            public FakeClusterClientSetup(FakeHttpMessages httpMessages) => 
                this.httpMessages = httpMessages;

            public void SetResponseBody(byte[] body) => httpMessages.ReplaceResponseBody(body);

            public void SetResponseCode(ResponseCode responseCode) => httpMessages.ReplaceResponseCode(responseCode);
        }
    }
}