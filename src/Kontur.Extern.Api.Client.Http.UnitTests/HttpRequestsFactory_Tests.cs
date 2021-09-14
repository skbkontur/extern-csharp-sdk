using System;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Api.Client.Http.Configurations;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Kontur.Extern.Api.Client.Http.Options;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Api.Client.Testing.Fakes.Http;
using Kontur.Extern.Api.Client.Testing.Fakes.Logging;
using Vostok.Clusterclient.Core.Model;
using Vostok.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.Http.UnitTests
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

                ClusterClientVerify.SentRequest.Should().NotBeNull();
                ClusterClientVerify.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClientVerify.SentRequest!.Method.Should().Be("GET");
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
                ClusterClientSetup.SetResponseBody(responseBytes);
                
                await CreateHttp().GetBytesAsync("/some-resource");

                ClusterClientVerify.SentRequest.Should().NotBeNull();
                ClusterClientVerify.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClientVerify.SentRequest!.Method.Should().Be("GET");
            }

            [Fact]
            public async Task GetBytesAsync_should_deserialize_bytes_from_response()
            {
                var responseBytes = new byte[] {1, 2, 3};
                ClusterClientSetup.SetResponseBody(responseBytes);
                
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

                ClusterClientVerify.SentRequest.Should().NotBeNull();
                ClusterClientVerify.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClientVerify.SentRequest!.Method.Should().Be("POST");
            }

            [Fact]
            public async Task SendAsync_should_serialize_request_DTO()
            {
                await CreateHttp().Post("/some-resource").WithObject(new {Data = "some request data"}).SendAsync();

                ClusterClientVerify.SentContentString.Should().Be(@"{""Data"":""some request data""}");
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
                ClusterClientVerify.SentContentString.Should().Be(@"{""Data"":""sent data""}");
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

                ClusterClientVerify.SentRequest.Should().NotBeNull();
                ClusterClientVerify.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClientVerify.SentRequest!.Method.Should().Be("PUT");
            }

            [Fact]
            public async Task Put_should_serialize_request_DTO()
            {
                await CreateHttp().Put("/some-resource").WithObject(new {Data = "some request data"}).SendAsync();

                ClusterClientVerify.SentContentString.Should().Be(@"{""Data"":""some request data""}");
            }

            [Fact]
            public async Task PutAsync_should_serialize_request_DTO_and_then_deserialize_response_DTO()
            {
                var expectedResponseDto = new ResponseDto {Data = "expected data"};

                var messageDto = await CreateHttp().PutAsync<RequestDto, ResponseDto>("/some-resource", new RequestDto {Data = "sent data"});

                messageDto.Should().BeEquivalentTo(expectedResponseDto);
                ClusterClientVerify.SentContentString.Should().Be(@"{""Data"":""sent data""}");
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

                ClusterClientVerify.SentRequest.Should().NotBeNull();
                ClusterClientVerify.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClientVerify.SentRequest!.Method.Should().Be("DELETE");
            }

            [Fact]
            public async Task DeleteAsync_should_send_DELETE_request_to_absolute_URI()
            {
                await CreateHttp().DeleteAsync("/some-resource");

                ClusterClientVerify.SentRequest.Should().NotBeNull();
                ClusterClientVerify.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
                ClusterClientVerify.SentRequest!.Method.Should().Be("DELETE");
            }
            
            protected override IHttpRequest MakeRequestForCommonTests(IHttpRequestsFactory http) => http.Delete("/some-resource");
        }
        
        public class Failover
        {
            private readonly FakeClusterClient fakeClient;
            private readonly FakeClusterClientSetup fakeClientSetup;
            private readonly FakeClusterClientVerify fakeClientVerify;
            private readonly ILog log;

            public Failover(ITestOutputHelper output)
            {
                log = new TestLog(output);
                fakeClient = CreateFakeClusterClient();
                fakeClientSetup = fakeClient.Setup;
                fakeClientVerify = fakeClient.Verify;
            }

            [Fact]
            public async Task Should_repeat_failed_requests_while_failover_prescribes_it()
            {
                var expectedUrl = new Uri("https://test/some");
                var http = CreateHttp(
                    (_, attempt) => Task.FromResult(attempt < 3
                        ? FailoverDecision.RepeatRequest
                        : FailoverDecision.LetItFail)
                );
                fakeClientSetup.SetResponseCode(ResponseCode.BadRequest);

                Func<Task> func = async () => await http.Get("/some").SendAsync();

                await func.Should().ThrowAsync<ContractException>();
                fakeClientVerify.SentRequests.Should().HaveCount(4);
                fakeClientVerify.SentRequests.Should()
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
                        fakeClientSetup.SetResponseCode(ResponseCode.Ok);
                    }
                    return Task.FromResult(FailoverDecision.RepeatRequest);
                });
                
                fakeClientSetup.SetResponseCode(ResponseCode.BadRequest);

                var response = await http.Post("/some").SendAsync();

                response.Status.IsSuccessful.Should().BeTrue();
            }

            private HttpRequestsFactory CreateHttp(
                FailoverAsync failover,
                Func<Request, TimeSpan, Task<Request>>? requestTransformAsync = null,
                Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler = null)
            {
                return HttpRequestsFactory_Tests.CreateHttp(fakeClient.Configuration, log, requestTransformAsync, errorResponseHandler, failover);
            }
        }

        public class ContentRange
        {
            private readonly ILog log;
            private readonly FakeClusterClient fakeClient;

            public ContentRange(ITestOutputHelper output)
            {
                log = new TestLog(output);
                fakeClient = CreateFakeClusterClient();
            }

            private FakeClusterClientVerify ClusterClientVerify => fakeClient.Verify;

            [Fact]
            public async Task Should_set_content_type_range_into_request()
            {
                await CreateHttp().Put("/some-resource")
                    .WithBytes(new byte[100])
                    .ContentRange(100, 199)
                    .SendAsync();

                ClusterClientVerify.SentRequest!.Headers!.ContentRange.Should().Be("bytes 100-199/*");
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

                ClusterClientVerify.SentRequest!.Headers!.ContentRange.Should().Be(expectedValue);
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

            private HttpRequestsFactory CreateHttp() => HttpRequestsFactory_Tests.CreateHttp(fakeClient.Configuration, log);
        }

        public abstract class VerbTestBase
        {
            private readonly TestLog log;
            private readonly FakeClusterClient fakeClient;

            protected VerbTestBase(ITestOutputHelper output)
            {
                log = new TestLog(output);
                fakeClient = CreateFakeClusterClient();
            }
            
            protected FakeClusterClientVerify ClusterClientVerify => fakeClient.Verify;
            protected FakeClusterClientSetup ClusterClientSetup => fakeClient.Setup;

            [Fact]
            public async Task Should_handle_wrong_response_by_the_given_error_handler()
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

                await sendRequest.Should().NotThrowAsync("the response is successful");

                ClusterClientSetup.SetResponseCode(ResponseCode.BadRequest);
                (await sendRequest.Should()
                    .ThrowAsync<ApplicationException>("the response is not successful and handled by the specified error response handler"))
                    .WithMessage(expectedErrorMessage);
            }
            
            [Fact]
            public async Task Should_allow_to_ignore_response_with_error_by_the_given_error_handler()
            {
                var http = CreateHttp(errorResponseHandler: response => new(response.Status.IsBadRequest));
                var request = MakeRequestForCommonTests(http);
                ClusterClientSetup.SetResponseCode(ResponseCode.BadRequest);

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
                ClusterClientSetup.SetResponseCode(ResponseCode.BadRequest);

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

                ClusterClientVerify.SentRequest.Should().NotBeNull();
                var userAgent = ClusterClientVerify.SentRequest!.Headers?.UserAgent;
                userAgent.Should().Be(expectedUserAgent);
            }

            protected abstract IHttpRequest MakeRequestForCommonTests(IHttpRequestsFactory http);

            protected HttpRequestsFactory CreateHttp(
                Func<Request, TimeSpan, Task<Request>>? requestTransformAsync = null,
                Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler = null)
            {
                return HttpRequestsFactory_Tests.CreateHttp(fakeClient.Configuration, log, requestTransformAsync, errorResponseHandler);
            }
        }

        private static HttpRequestsFactory CreateHttp(
            IHttpClientConfiguration configuration,
            ILog log,
            Func<Request, TimeSpan, Task<Request>>? requestTransformAsync = null,
            Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler = null,
            FailoverAsync? failover = null)
        {
            return new(
                configuration,
                new RequestTimeouts(),
                requestTransformAsync,
                errorResponseHandler,
                failover,
                new SystemTextJsonSerializerFactory().CreateSerializer(),
                log
            );
        }

        private static FakeClusterClient CreateFakeClusterClient()
        {
            var client = new FakeClusterClient();
            client.Setup.SetJsonResponse(ResponseCode.Ok, @"{""Data"": ""expected data""}");
            return client;
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