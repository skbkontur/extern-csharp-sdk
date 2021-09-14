using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Api.Client.Http.Constants;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Api.Client.Testing.Helpers;
using Vostok.Clusterclient.Core.Model;
using Xunit;

namespace Kontur.Extern.Api.Client.Http.UnitTests
{
    public class HttpResponse_Tests
    {
        [Fact]
        public async Task GetBytes_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse();

            Func<Task> func = async () => await httpResponse.GetBytesAsync();

            await func.Should().ThrowAsync<ContractException>();
        }
        
        [Fact]
        public async Task GetBytes_should_extract_bytes_from_content()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(new Content(bytes));

            var actualBytes = await httpResponse.GetBytesAsync();
            
            actualBytes.Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public async Task GetBytes_should_extract_bytes_from_memory_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new MemoryStream(bytes));

            var actualBytes = await httpResponse.GetBytesAsync();
            
            actualBytes.Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public async Task GetBytes_should_extract_bytes_from_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new BufferedStream(new MemoryStream(bytes)));

            var actualBytes = await httpResponse.GetBytesAsync();
            
            actualBytes.Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public async Task GetBytesSegment_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse();

            Func<Task> func = async () => await httpResponse.GetBytesSegmentAsync();

            await func.Should().ThrowAsync<ContractException>();
        }
        
        [Fact]
        public async Task GetBytesSegment_should_extract_bytes_from_content()
        {
            var bytes = new byte[] {0, 1, 2, 3, 4};
            var expectedBytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(new Content(new ArraySegment<byte>(bytes, 1, 3)));

            var actualBytes = await httpResponse.GetBytesSegmentAsync();
            
            actualBytes.ToArray().Should().BeEquivalentTo(expectedBytes);
        }
        
        [Fact]
        public async Task GetBytesSegment_should_extract_bytes_from_memory_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new MemoryStream(bytes));

            var actualBytes = await httpResponse.GetBytesSegmentAsync();
            
            actualBytes.ToArray().Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public async Task GetBytesSegment_should_extract_bytes_from_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new BufferedStream(new MemoryStream(bytes)));

            var actualBytes = await httpResponse.GetBytesSegmentAsync();
            
            actualBytes.ToArray().Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public void GetStream_should_return_response_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new BufferedStream(new MemoryStream(bytes)));

            var actualStream = httpResponse.GetStream();
            
            actualStream.ReadAllBytes().Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public void GetStream_should_return_stream_from_buffered_content()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(new Content(bytes));

            var stream = httpResponse.GetStream();
            
            stream.ReadAllBytes().Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public async Task GetMessage_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse(
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            Func<Task> func = async () => await httpResponse.GetMessageAsync<Dto>();

            await func.Should().ThrowAsync<ContractException>();
        }
        
        [Fact]
        public async Task GetMessage_should_deserialize_response_stream_to_DTO()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                stream: ToStream(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            var dto = await httpResponse.GetMessageAsync<Dto>();
            
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Fact]
        public async Task GetMessage_should_deserialize_response_content_to_DTO()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            var dto = await httpResponse.GetMessageAsync<Dto>();
            
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Fact]
        public async Task GetMessage_should_fail_when_content_type_is_not_a_json()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "application/pdf"));

            Func<Task> func = async () => await httpResponse.GetMessageAsync<Dto>();

            await func.Should().ThrowAsync<ContractException>();
        }
        
        [Fact]
        public async Task GetMessage_should_fail_when_content_type_is_plain_text()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text"));

            Func<Task> func = async () => await httpResponse.GetMessageAsync<Dto>();

            await func.Should().ThrowAsync<ContractException>();
        }
        
        [Fact]
        public async Task GetMessage_should_return_body_string_when_a_content_type_is_plain_text_but_the_return_type_is_a_string()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text;encoding=utf-8"));

            var message = await httpResponse.GetMessageAsync<string>();

            message.Should().Be(json);
        }
        
        [Fact]
        public async Task GetMessage_should_deserialize_response_content_to_DTO_if_content_type_is_json_with_charset()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "application/json; charset=utf-8"));

            var dto = await httpResponse.GetMessageAsync<Dto>();
            
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Fact]
        public async Task GetMessage_should_fail_when_content_type_is_absent()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(ToContent(json));

            Func<Task> func = async () => await httpResponse.GetMessageAsync<Dto>();

            await func.Should().ThrowAsync<ContractException>();
        }
        
        [Fact]
        public async Task TryGetMessage_should_return_error_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse(
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            await ShouldReturnErrorWhenTryGetMessageOfDto(httpResponse);
        }

        [Fact]
        public async Task TryGetMessage_should_deserialize_response_stream_to_DTO()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                stream: ToStream(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            var dto = await httpResponse.TryGetMessageAsync<Dto>();

            dto.Should().NotBeNull().And.BeEquivalentTo(expectedDto);
        }
        
        [Fact]
        public async Task TryGetMessage_should_return_error_when_content_type_is_not_a_json()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "application/pdf"));

            await ShouldReturnErrorWhenTryGetMessageOfDto(httpResponse);
        }
        
        [Fact]
        public async Task TryGetMessage_should_deserialize_response_content_to_DTO_if_content_type_is_json_with_charset()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "application/json; charset=utf-8"));

            var dto = await httpResponse.TryGetMessageAsync<Dto>();

            dto.Should().NotBeNull().And.BeEquivalentTo(expectedDto);
        }
        
        [Fact]
        public async Task TryGetMessage_should_return_error_when_content_type_is_absent()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(ToContent(json));

            await ShouldReturnErrorWhenTryGetMessageOfDto(httpResponse);
        }
        
        [Fact]
        public async Task TryGetMessage_should_return_error_when_a_content_type_is_plain_text()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text;charset=utf-8"));

            await ShouldReturnErrorWhenTryGetMessageOfDto(httpResponse);
        }
        
        [Fact]
        public async Task TryGetMessage_should_return_body_string_when_a_content_type_is_plain_text_but_the_return_type_is_a_string_and_response_has_content()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text;charset=utf-8"));

            var message = await httpResponse.TryGetMessageAsync<string>();

            message.Should().NotBeNull().And.Be(json);
        }
        
        [Fact]
        public async Task TryGetMessage_should_return_body_string_when_a_content_type_is_plain_text_but_the_return_type_is_a_string_and_response_has_stream()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                stream: ToStream(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text;charset=utf-8"));

            var message = await httpResponse.TryGetMessageAsync<string>();

            message.Should().NotBeNull().And.Be(json);
        }
        
        [Fact]
        public async Task TryGetMessage_should_return_error_when_a_content_type_is_plain_text_and_return_type_string_but_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse(
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text;charset=utf-8"));

            var message = await httpResponse.TryGetMessageAsync<string>();

            message.Should().BeNull();
        }

        private static async Task ShouldReturnErrorWhenTryGetMessageOfDto(IHttpResponse httpResponse)
        {
            var dto = await httpResponse.TryGetMessageAsync<Dto>();

            dto.Should().BeNull();
        }
        
        [Fact]
        public async Task GetString_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse(headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            Func<Task> func = async () => await httpResponse.GetStringAsync();

            await func.Should().ThrowAsync<ContractException>();
        }
        
        [Fact]
        public async Task GetString_should_deserialize_response_memory_stream_to_DTO()
        {
            const string expectedBody = "some data";
            var httpResponse = CreateHttpResponse(stream: ToStream(expectedBody));

            var body = await httpResponse.GetStringAsync();
            
            body.Should().BeEquivalentTo(expectedBody);
        }
        
        [Fact]
        public async Task GetString_should_deserialize_response_stream_to_DTO()
        {
            const string expectedBody = "some data";
            var httpResponse = CreateHttpResponse(stream: new BufferedStream(ToStream(expectedBody)));

            var body = await httpResponse.GetStringAsync();
            
            body.Should().BeEquivalentTo(expectedBody);
        }
        
        [Fact]
        public async Task GetString_should_deserialize_response_content_to_DTO()
        {
            const string expectedBody = "some data";
            var httpResponse = CreateHttpResponse(ToContent(expectedBody));

            var body = await httpResponse.GetStringAsync();
            
            body.Should().BeEquivalentTo(expectedBody);
        }

        private static HttpResponse CreateHttpResponse(Content? content = null, Stream? stream = null, Headers? headers = null)
        {
            var dummyRequest = Request.Get(new Uri("/dummy", UriKind.Relative));
            return new HttpResponse(
                dummyRequest,
                new Response(ResponseCode.Ok, content, headers, stream),
                new SystemTextJsonSerializerFactory().CreateSerializer()
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