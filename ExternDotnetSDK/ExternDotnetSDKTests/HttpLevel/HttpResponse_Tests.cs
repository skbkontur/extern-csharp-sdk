#nullable enable
using System;
using System.IO;
using System.Text;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.HttpLevel.ClusterClientAdapters;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using NUnit.Framework;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Tests.HttpLevel
{
    [TestFixture]
    internal class HttpResponse_Tests
    {
        [Test]
        public void GetBytes_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse();

            Action action = () => httpResponse.GetBytes();

            action.Should().Throw<ContractException>();
        }
        
        [Test]
        public void GetBytes_should_extract_bytes_from_content()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(new Content(bytes));

            var actualBytes = httpResponse.GetBytes();
            
            actualBytes.Should().BeEquivalentTo(bytes);
        }
        
        [Test]
        public void GetBytes_should_extract_bytes_from_memory_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new MemoryStream(bytes));

            var actualBytes = httpResponse.GetBytes();
            
            actualBytes.Should().BeEquivalentTo(bytes);
        }
        
        [Test]
        public void GetBytes_should_extract_bytes_from_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new BufferedStream(new MemoryStream(bytes)));

            var actualBytes = httpResponse.GetBytes();
            
            actualBytes.Should().BeEquivalentTo(bytes);
        }
        
        [Test]
        public void GetMessage_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse(
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            Action action = () => httpResponse.GetMessage<Dto>();

            action.Should().Throw<ContractException>();
        }
        
        [Test]
        public void GetMessage_should_deserialize_response_stream_to_DTO()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                stream: ToStream(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            var dto = httpResponse.GetMessage<Dto>();
            
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Test]
        public void GetMessage_should_deserialize_response_content_to_DTO()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            var dto = httpResponse.GetMessage<Dto>();
            
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Test]
        public void GetMessage_should_fail_when_content_type_is_not_a_json()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "application/pdf"));

            Action action = () => httpResponse.GetMessage<Dto>();

            action.Should().Throw<ContractException>();
        }
        
        [Test]
        public void GetMessage_should_fail_when_content_type_is_absent()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(ToContent(json));

            Action action = () => httpResponse.GetMessage<Dto>();

            action.Should().Throw<ContractException>();
        }
        
        [Test]
        public void GetString_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse(headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            Action action = () => httpResponse.GetString();

            action.Should().Throw<ContractException>();
        }
        
        [Test]
        public void GetString_should_deserialize_response_memory_stream_to_DTO()
        {
            const string expectedBody = "some data";
            var httpResponse = CreateHttpResponse(stream: ToStream(expectedBody));

            var body = httpResponse.GetString();
            
            body.Should().BeEquivalentTo(expectedBody);
        }
        
        [Test]
        public void GetString_should_deserialize_response_stream_to_DTO()
        {
            const string expectedBody = "some data";
            var httpResponse = CreateHttpResponse(stream: new BufferedStream(ToStream(expectedBody)));

            var body = httpResponse.GetString();
            
            body.Should().BeEquivalentTo(expectedBody);
        }
        
        [Test]
        public void GetString_should_deserialize_response_content_to_DTO()
        {
            const string expectedBody = "some data";
            var httpResponse = CreateHttpResponse(ToContent(expectedBody));

            var body = httpResponse.GetString();
            
            body.Should().BeEquivalentTo(expectedBody);
        }

        private static HttpResponse CreateHttpResponse(Content? content = null, Stream? stream = null, Headers? headers = null)
        {
            var dummyRequest = Request.Get(new Uri("/dummy", UriKind.Relative));
            return new HttpResponse(
                dummyRequest,
                new Response(ResponseCode.Ok, content, headers, stream),
                new JsonSerializer()
            );
        }

        private static MemoryStream ToStream(string json) => new(Encoding.UTF8.GetBytes(json));
        
        private static Content ToContent(string json) => new(Encoding.UTF8.GetBytes(json));

        private class Dto
        {
            [UsedImplicitly]
            public string? Data { get; set; }
        }
    }
}