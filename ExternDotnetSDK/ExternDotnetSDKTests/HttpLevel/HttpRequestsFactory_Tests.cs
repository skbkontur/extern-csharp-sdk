#nullable enable
using System;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.HttpLevel;
using Kontur.Extern.Client.HttpLevel.ClusterClientAdapters;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Kontur.Extern.Client.Tests.Fakes.Http;
using Kontur.Extern.Client.Tests.Fakes.Logging;
using NUnit.Framework;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Tests.HttpLevel
{
    [TestFixture]
    internal class HttpRequestsFactory_Tests
    {
        [TestFixture]
        public class Get : Base
        {
            [Test]
            public async Task Get_should_send_GET_request_to_absolute_URI()
            {
                await CreateHttp().Get("/some-resource").SendAsync();

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("GET");
            }

            [Test]
            public async Task GetAsync_should_deserialize_response_as_specified_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await CreateHttp().GetAsync<ResponseDto>("/some-resource");

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
            }

            [Test]
            public async Task GetBytesAsync_should_send_GET_request_to_absolute_URI()
            {
                var responseBytes = new byte[] {1, 2, 3};
                ClusterClient.SetResponseBody(responseBytes);
                
                await CreateHttp().GetBytesAsync("/some-resource");

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("GET");
            }

            [Test]
            public async Task GetBytesAsync_should_deserialize_bytes_from_response()
            {
                var responseBytes = new byte[] {1, 2, 3};
                ClusterClient.SetResponseBody(responseBytes);
                
                var bytes = await CreateHttp().GetBytesAsync("/some-resource");

                bytes.Should().BeEquivalentTo(responseBytes);
            }

            protected override IHttpRequest MakeRequestForCommonTests(HttpRequestsFactory http) => http.Get("/some-resource");
        }

        [TestFixture]
        public class Post : Base
        {
            [Test]
            public async Task SendAsync_should_send_POST_request_to_absolute_URI()
            {
                await CreateHttp().Post("/some-resource").SendAsync();

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("POST");
            }

            [Test]
            public async Task SendAsync_should_serialize_request_DTO()
            {
                await CreateHttp().Post("/some-resource").WithObject(new {Data = "some request data"}).SendAsync();

                ClusterClient.SentContentString.Should().Be(@"{""Data"":""some request data""}");
            }

            [Test]
            public async Task should_deserialize_response_as_specified_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await CreateHttp().PostAsync<ResponseDto>("/some-resource");

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
            }

            [Test]
            public async Task PostAsync_should_serialize_request_DTO_and_then_deserialize_response_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await CreateHttp().PostAsync<RequestDto, ResponseDto>("/some-resource", new RequestDto {Data = "sent data"});

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
                ClusterClient.SentContentString.Should().Be(@"{""Data"":""sent data""}");
            }

            protected override IHttpRequest MakeRequestForCommonTests(HttpRequestsFactory http) => http.Post("/some-resource");
        }

        [TestFixture]
        public class Put : Base
        {
            [Test]
            public async Task SendAsync_should_send_PUT_request_to_absolute_URI()
            {
                await CreateHttp().Put("/some-resource").SendAsync();

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("PUT");
            }

            [Test]
            public async Task Put_should_serialize_request_DTO()
            {
                await CreateHttp().Put("/some-resource").WithObject(new {Data = "some request data"}).SendAsync();

                ClusterClient.SentContentString.Should().Be(@"{""Data"":""some request data""}");
            }

            [Test]
            public async Task PutAsync_should_serialize_request_DTO_and_then_deserialize_response_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await CreateHttp().PutAsync<RequestDto, ResponseDto>("/some-resource", new RequestDto {Data = "sent data"});

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
                ClusterClient.SentContentString.Should().Be(@"{""Data"":""sent data""}");
            }
            
            protected override IHttpRequest MakeRequestForCommonTests(HttpRequestsFactory http) => http.Put("/some-resource");
        }

        [TestFixture]
        public class Delete : Base
        {
            [Test]
            public async Task SendAsync_should_send_DELETE_request_to_absolute_URI()
            {
                await CreateHttp().Delete("/some-resource").SendAsync();

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("DELETE");
            }

            [Test]
            public async Task DeleteAsync_should_send_DELETE_request_to_absolute_URI()
            {
                await CreateHttp().DeleteAsync("/some-resource");

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("DELETE");
            }
            
            protected override IHttpRequest MakeRequestForCommonTests(HttpRequestsFactory http) => http.Delete("/some-resource");
        }

        public abstract class Base
        {
            internal FakeClusterClient ClusterClient = null!;

            [SetUp]
            public void SetUp() => ClusterClient = CreateFakeClusterClient();

            [Test]
            public void Should_handle_wrong_response_by_the_given_error_handler()
            {
                const string expectedErrorMessage = "Expected exception";
                var http = CreateHttp(errorResponseHandler: response =>
                {
                    if (!response.Status.IsSuccessful)
                    {
                        throw new ApplicationException(expectedErrorMessage);
                    }
                });
                var request = MakeRequestForCommonTests(http);
                Func<Task> sendRequest = async () => await request.SendAsync();

                sendRequest.Should().NotThrow("the response is successful");

                ClusterClient.SetResponseCode(ResponseCode.BadRequest);
                sendRequest.Should()
                    .Throw<ApplicationException>("the response is not successful and handled by the specified error response handler")
                    .WithMessage(expectedErrorMessage);
            }

            [Test]
            public async Task Should_transform_sent_request_by_the_given_transformation_function()
            {
                const string expectedUserAgent = "the-expected-user-agent";
                var http = CreateHttp((r, _) => Task.FromResult(r.WithUserAgentHeader(expectedUserAgent)));
                var request = MakeRequestForCommonTests(http);

                await request.SendAsync();

                ClusterClient.SentRequest.Should().NotBeNull();
                var userAgent = ClusterClient.SentRequest!.Headers?.UserAgent;
                userAgent.Should().Be(expectedUserAgent);
            }

            protected abstract IHttpRequest MakeRequestForCommonTests(HttpRequestsFactory http);

            protected HttpRequestsFactory CreateHttp(
                Func<Request, TimeSpan, Task<Request>>? requestTransformAsync = null,
                Action<IHttpResponse>? errorResponseHandler = null)
            {
                return new(
                    new RequestSendingOptions(),
                    requestTransformAsync,
                    errorResponseHandler,
                    ClusterClient,
                    new JsonSerializer(),
                    new TestLog()
                );
            }

            private static FakeClusterClient CreateFakeClusterClient() =>
                FakeClusterClientFactory
                    .WithBaseUrl("https://test/")
                    .WithJsonResponse(ResponseCode.Ok, @"{""Data"": ""expected data""}")
                    .CreateFakeClusterClient();
        }

        private class ResponseDto
        {
            [UsedImplicitly]
            public string? Data { get; set; }
        }

        private class RequestDto
        {
            [UsedImplicitly]
            public string? Data { get; set; }
        }
    }
}