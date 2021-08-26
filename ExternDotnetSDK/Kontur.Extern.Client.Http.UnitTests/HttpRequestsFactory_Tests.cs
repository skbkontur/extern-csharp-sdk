using System;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Client.Http.Exceptions;
using Kontur.Extern.Client.Http.Options;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Testing.Fakes.Http;
using Kontur.Extern.Client.Testing.Fakes.Logging;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.Http.UnitTests
{
    public static class HttpRequestsFactory_Tests
    {
        public class Get : VerbTestBase
        {
            public Get(ITestOutputHelper output)
                : base(output)
            {
            }
            
            [Fact]
            public async Task Get_should_send_GET_request_to_absolute_URI()
            {
                await CreateHttp().Get("/some-resource").SendAsync();

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("GET");
            }

            [Fact]
            public async Task GetAsync_should_deserialize_response_as_specified_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await CreateHttp().GetAsync<ResponseDto>("/some-resource");

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
            }

            [Fact]
            public async Task GetBytesAsync_should_send_GET_request_to_absolute_URI()
            {
                var responseBytes = new byte[] {1, 2, 3};
                ClusterClient.SetResponseBody(responseBytes);
                
                await CreateHttp().GetBytesAsync("/some-resource");

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("GET");
            }

            [Fact]
            public async Task GetBytesAsync_should_deserialize_bytes_from_response()
            {
                var responseBytes = new byte[] {1, 2, 3};
                ClusterClient.SetResponseBody(responseBytes);
                
                var bytes = await CreateHttp().GetBytesAsync("/some-resource");

                bytes.Should().BeEquivalentTo(responseBytes);
            }

            protected override IHttpRequest MakeRequestForCommonTests(IHttpRequestsFactory http) => http.Get("/some-resource");
        }

        public class Post : VerbTestBase
        {
            public Post(ITestOutputHelper output)
                : base(output)
            {
            }
            
            [Fact]
            public async Task SendAsync_should_send_POST_request_to_absolute_URI()
            {
                await CreateHttp().Post("/some-resource").SendAsync();

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("POST");
            }

            [Fact]
            public async Task SendAsync_should_serialize_request_DTO()
            {
                await CreateHttp().Post("/some-resource").WithObject(new {Data = "some request data"}).SendAsync();

                ClusterClient.SentContentString.Should().Be(@"{""Data"":""some request data""}");
            }

            [Fact]
            public async Task Should_deserialize_response_as_specified_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await CreateHttp().PostAsync<ResponseDto>("/some-resource");

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
            }

            [Fact]
            public async Task PostAsync_should_serialize_request_DTO_and_then_deserialize_response_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await CreateHttp().PostAsync<RequestDto, ResponseDto>("/some-resource", new RequestDto {Data = "sent data"});

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
                ClusterClient.SentContentString.Should().Be(@"{""Data"":""sent data""}");
            }

            protected override IHttpRequest MakeRequestForCommonTests(IHttpRequestsFactory http) => http.Post("/some-resource");
        }

        public class Put : VerbTestBase
        {
            public Put(ITestOutputHelper output)
                : base(output)
            {
            }
            
            [Fact]
            public async Task SendAsync_should_send_PUT_request_to_absolute_URI()
            {
                await CreateHttp().Put("/some-resource").SendAsync();

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("PUT");
            }

            [Fact]
            public async Task Put_should_serialize_request_DTO()
            {
                await CreateHttp().Put("/some-resource").WithObject(new {Data = "some request data"}).SendAsync();

                ClusterClient.SentContentString.Should().Be(@"{""Data"":""some request data""}");
            }

            [Fact]
            public async Task PutAsync_should_serialize_request_DTO_and_then_deserialize_response_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await CreateHttp().PutAsync<RequestDto, ResponseDto>("/some-resource", new RequestDto {Data = "sent data"});

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
                ClusterClient.SentContentString.Should().Be(@"{""Data"":""sent data""}");
            }
            
            protected override IHttpRequest MakeRequestForCommonTests(IHttpRequestsFactory http) => http.Put("/some-resource");
        }

        public class Delete : VerbTestBase
        {
            public Delete(ITestOutputHelper output)
                : base(output)
            {
            }
            
            [Fact]
            public async Task SendAsync_should_send_DELETE_request_to_absolute_URI()
            {
                await CreateHttp().Delete("/some-resource").SendAsync();

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("DELETE");
            }

            [Fact]
            public async Task DeleteAsync_should_send_DELETE_request_to_absolute_URI()
            {
                await CreateHttp().DeleteAsync("/some-resource");

                ClusterClient.SentRequest.Should().NotBeNull();
                ClusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClient.SentRequest!.Method.Should().Be("DELETE");
            }
            
            protected override IHttpRequest MakeRequestForCommonTests(IHttpRequestsFactory http) => http.Delete("/some-resource");
        }
        
        public class Failover
        {
            private readonly FakeClusterClient clusterClient;

            public Failover(ITestOutputHelper output)
            {
                var log = new TestLog(output);
                clusterClient = CreateFakeClusterClient(log);
            }

            [Fact]
            public void Should_repeat_failed_requests_while_failover_prescribes_it()
            {
                var expectedUrl = new Uri("https://test/some");
                var http = CreateHttp(
                    (_, attempt) => Task.FromResult(attempt < 3
                        ? FailoverDecision.RepeatRequest
                        : FailoverDecision.LetItFail)
                );
                clusterClient.SetResponseCode(ResponseCode.BadRequest);

                Func<Task> func = async () => await http.Get("/some").SendAsync();

                func.Should().Throw<ContractException>();
                clusterClient.SentRequests.Should().HaveCount(4);
                clusterClient.SentRequests.Should()
                    .OnlyContain(request => request.Url == expectedUrl &&
                                            request.Method == RequestMethods.Get);
            }

            [Fact]
            public async Task Should_repeat_the_request_by_failover_decision_until_response_is_unsuccessful()
            {
                var http = CreateHttp((_, attempt) =>
                {
                    if (attempt >= 3)
                    {
                        clusterClient.SetResponseCode(ResponseCode.Ok);
                    }
                    return Task.FromResult(FailoverDecision.RepeatRequest);
                });
                
                clusterClient.SetResponseCode(ResponseCode.BadRequest);

                var response = await http.Post("/some").SendAsync();

                response.Status.IsSuccessful.Should().BeTrue();
            }

            private HttpRequestsFactory CreateHttp(
                FailoverAsync failover,
                Func<Request, TimeSpan, Task<Request>>? requestTransformAsync = null,
                Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler = null)
            {
                return HttpRequestsFactory_Tests.CreateHttp(clusterClient, requestTransformAsync, errorResponseHandler, failover);
            }
        }

        public class ContentRange
        {
            private readonly FakeClusterClient clusterClient;

            public ContentRange(ITestOutputHelper output)
            {
                var log = new TestLog(output);
                clusterClient = CreateFakeClusterClient(log);
            }
            
            [Fact]
            public async Task Should_set_content_type_range_into_request()
            {
                await CreateHttp().Put("/some-resource")
                    .WithBytes(new byte[100])
                    .ContentRange(100, 199)
                    .SendAsync();

                clusterClient.SentRequest!.Headers!.ContentRange.Should().Be("bytes 100-199/*");
            }
            
            [Theory]
            [InlineData(0, 2, 3, "bytes 0-2/3")]
            [InlineData(100, 200, 3000, "bytes 100-200/3000")]
            public async Task Should_set_content_type_range_with_total_length_into_request(int from, int to, int total, string expectedValue)
            {
                await CreateHttp().Post("/some-resource")
                    .WithBytes(new byte[to - from + 1])
                    .ContentRange(from, to, total)
                    .SendAsync();

                clusterClient.SentRequest!.Headers!.ContentRange.Should().Be(expectedValue);
            }
            
            [Theory]
            [InlineData(0, 2, 4)]
            [InlineData(0, 2, 2)]
            [InlineData(100, 200, 200)]
            [InlineData(100, 200, 50)]
            public void Should_fail_when_given_inconsistent_range_to_content_length(int rangeStart, int rangeEnd, int contentLength)
            {
                var payloadSpecifiedRequest = CreateHttp().Post("/some-resource").WithBytes(new byte[contentLength]);

                Action action = () => payloadSpecifiedRequest.ContentRange(rangeStart, rangeEnd);

                action.Should().Throw<ArgumentException>();
            }
            
            [Theory]
            [InlineData(100, 200, 200, 300)]
            [InlineData(100, 200, 50, 300)]
            [InlineData(100, 200, 100, 50)]
            [InlineData(150, 50, 100, 300)]
            public void Should_fail_when_given_inconsistent_range_to_content_length_and_total_length(int rangeStart, int rangeEnd, int contentLength, int totalLength)
            {
                var payloadSpecifiedRequest = CreateHttp().Put("/some-resource").WithBytes(new byte[contentLength]);

                Action action = () => payloadSpecifiedRequest.ContentRange(rangeStart, rangeEnd, totalLength);

                action.Should().Throw<ArgumentException>();
            }

            private HttpRequestsFactory CreateHttp() => HttpRequestsFactory_Tests.CreateHttp(clusterClient);
        }

        public abstract class VerbTestBase
        {
            internal readonly FakeClusterClient ClusterClient;

            protected VerbTestBase(ITestOutputHelper output)
            {
                var log = new TestLog(output);
                ClusterClient = CreateFakeClusterClient(log);
            }

            [Fact]
            public void Should_handle_wrong_response_by_the_given_error_handler()
            {
                const string expectedErrorMessage = "Expected exception";
                var http = CreateHttp(errorResponseHandler: response =>
                {
                    if (!response.Status.IsSuccessful)
                        throw new ApplicationException(expectedErrorMessage);

                    return new(false);
                });
                var request = MakeRequestForCommonTests(http);
                Func<Task> sendRequest = async () => await request.SendAsync();

                sendRequest.Should().NotThrow("the response is successful");

                ClusterClient.SetResponseCode(ResponseCode.BadRequest);
                sendRequest.Should()
                    .Throw<ApplicationException>("the response is not successful and handled by the specified error response handler")
                    .WithMessage(expectedErrorMessage);
            }
            
            [Fact]
            public async Task Should_allow_to_ignore_response_with_error_by_the_given_error_handler()
            {
                var http = CreateHttp(errorResponseHandler: response => new(response.Status.IsBadRequest));
                var request = MakeRequestForCommonTests(http);
                ClusterClient.SetResponseCode(ResponseCode.BadRequest);

                var httpResponse = await request.SendAsync();

                httpResponse.Status.IsBadRequest.Should().BeTrue();
            }
            
            [Fact]
            public async Task Should_allow_to_ignore_response_with_error_by_the_given_filtering_delegate()
            {
                var http = CreateHttp(errorResponseHandler: response =>
                {
                    if (!response.Status.IsSuccessful)
                        throw new ApplicationException("Unexpected");

                    return new(false);
                });
                var request = MakeRequestForCommonTests(http);
                ClusterClient.SetResponseCode(ResponseCode.BadRequest);

                var httpResponse = await request.SendAsync(ignoreResponseErrors: response => response.Status.IsBadRequest);

                httpResponse.Status.IsBadRequest.Should().BeTrue();
            }

            [Fact]
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

            protected abstract IHttpRequest MakeRequestForCommonTests(IHttpRequestsFactory http);

            protected HttpRequestsFactory CreateHttp(
                Func<Request, TimeSpan, Task<Request>>? requestTransformAsync = null,
                Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler = null)
            {
                return HttpRequestsFactory_Tests.CreateHttp(ClusterClient, requestTransformAsync, errorResponseHandler);
            }
        }

        private static HttpRequestsFactory CreateHttp(
            IClusterClient clusterClient,
            Func<Request, TimeSpan, Task<Request>>? requestTransformAsync = null,
            Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler = null,
            FailoverAsync? failover = null)
        {
            return new(
                new RequestTimeouts(),
                requestTransformAsync,
                errorResponseHandler,
                failover,
                clusterClient,
                new SystemTextJsonSerializer()
            );
        }

        private static FakeClusterClient CreateFakeClusterClient(ILog log) =>
            FakeClusterClientFactory
                .WithBaseUrl("https://test/")
                .WithJsonResponse(ResponseCode.Ok, @"{""Data"": ""expected data""}")
                .CreateFakeClusterClient(log);

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