#nullable enable
using System;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.HttpLevel;
using Kontur.Extern.Client.HttpLevel.ClusterClientAdapters;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Kontur.Extern.Client.Tests.Fakes.Auth;
using Kontur.Extern.Client.Tests.Fakes.Http;
using Kontur.Extern.Client.Tests.Fakes.Logging;
using NUnit.Framework;
using Vostok.Clusterclient.Core;
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
                await http.Get("/some-resource").SendAsync();

                clusterClient.SentRequest.Should().NotBeNull();
                clusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                clusterClient.SentRequest!.Method.Should().Be("GET");
            }

            [Test]
            public async Task GetAsync_should_deserialize_response_as_specified_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await http.GetAsync<ResponseDto>("/some-resource");

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
            }

            [Test]
            public async Task GetBytesAsync_should_send_GET_request_to_absolute_URI()
            {
                var responseBytes = new byte[] {1, 2, 3};
                clusterClient.SetResponseBody(responseBytes);
                
                await http.GetBytesAsync("/some-resource");

                clusterClient.SentRequest.Should().NotBeNull();
                clusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                clusterClient.SentRequest!.Method.Should().Be("GET");
            }

            [Test]
            public async Task GetBytesAsync_should_deserialize_bytes_from_response()
            {
                var responseBytes = new byte[] {1, 2, 3};
                clusterClient.SetResponseBody(responseBytes);
                
                var bytes = await http.GetBytesAsync("/some-resource");

                bytes.Should().BeEquivalentTo(responseBytes);
            }
        }

        [TestFixture]
        public class Post : Base
        {
            [Test]
            public async Task SendAsync_should_send_POST_request_to_absolute_URI()
            {
                await http.Post("/some-resource").SendAsync();

                clusterClient.SentRequest.Should().NotBeNull();
                clusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                clusterClient.SentRequest!.Method.Should().Be("POST");
            }

            [Test]
            public async Task SendAsync_should_serialize_request_DTO()
            {
                await http.Post("/some-resource").WithObject(new {Data = "some request data"}).SendAsync();

                clusterClient.SentRequest?.Content?.ToString().Should().Be(@"{Data:""some request data""}");
            }

            [Test]
            public async Task should_deserialize_response_as_specified_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await http.PostAsync<ResponseDto>("/some-resource");

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
            }

            [Test]
            public async Task PostAsync_should_serialize_request_DTO_and_then_deserialize_response_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await http.PostAsync<RequestDto, ResponseDto>("/some-resource", new RequestDto {Data = "sent data"});

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
                clusterClient.SentRequest?.Content?.ToString().Should().Be(@"{Data:""sent data""}");
            }
        }

        [TestFixture]
        public class Put : Base
        {
            [Test]
            public async Task SendAsync_should_send_PUT_request_to_absolute_URI()
            {
                await http.Put("/some-resource").SendAsync();

                clusterClient.SentRequest.Should().NotBeNull();
                clusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                clusterClient.SentRequest!.Method.Should().Be("PUT");
            }

            [Test]
            public async Task Put_should_serialize_request_DTO()
            {
                await http.Put("/some-resource").WithObject(new {Data = "some request data"}).SendAsync();

                clusterClient.SentRequest?.Content?.ToString().Should().Be(@"{Data:""some request data""}");
            }

            [Test]
            public async Task PutAsync_should_serialize_request_DTO_and_then_deserialize_response_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await http.PutAsync<RequestDto, ResponseDto>("/some-resource", new RequestDto {Data = "sent data"});

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
                clusterClient.SentRequest?.Content?.ToString().Should().Be(@"{Data:""sent data""}");
            }
        }

        [TestFixture]
        public class Delete : Base
        {
            [Test]
            public async Task SendAsync_should_send_DELETE_request_to_absolute_URI()
            {
                await http.Delete("/some-resource").SendAsync();

                clusterClient.SentRequest.Should().NotBeNull();
                clusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                clusterClient.SentRequest!.Method.Should().Be("DELETE");
            }

            [Test]
            public async Task DeleteAsync_should_send_DELETE_request_to_absolute_URI()
            {
                await http.DeleteAsync("/some-resource");

                clusterClient.SentRequest.Should().NotBeNull();
                clusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                clusterClient.SentRequest!.Method.Should().Be("DELETE");
            }
        }

        public abstract class Base
        {
            internal FakeClusterClient clusterClient = null!;
            internal HttpRequestsFactory http = null!;

            [SetUp]
            public void SetUp()
            {
                clusterClient = CreateFakeClusterClient();
                http = CreateHttp(clusterClient);
            }

            private static FakeClusterClient CreateFakeClusterClient() =>
                FakeClusterClientFactory
                    .WithBaseUrl("https://test/")
                    .WithJsonResponse(ResponseCode.Ok, @"{""Data"": ""expected data""}")
                    .CreateFakeClusterClient();

            private static HttpRequestsFactory CreateHttp(IClusterClient clusterClient) => new(
                new RequestSendingOptions(),
                new AuthenticationOptions("apiKey", new FakeAuthProvider()),
                clusterClient,
                new JsonSerializer(),
                new TestLog()
            );
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