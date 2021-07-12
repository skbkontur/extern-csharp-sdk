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
        [Test]
        public async Task Get_should_send_request_to_absolute_uri()
        {
            var clusterClient = FakeClusterClientFactory
                .WithBaseUrl("https://test/")
                .WithJsonResponse(ResponseCode.Ok, @"{""Data"": ""expected data""}")
                .CreateFakeClusterClient();
            var http = CreateHttp(clusterClient);
            
            await http.GetAsync<MessageDto>("/some-resource");

            clusterClient.SentRequest.Should().NotBeNull();
            clusterClient.SentRequest!.Url.Should().Be(new Uri("https://test/some-resource"));
        }
        
        [Test]
        public async Task Get_should_receive_deserialized_message()
        {
            var expectedMessageDto = new MessageDto {Data = "expected data"};

            var clusterClient = FakeClusterClientFactory
                .WithDefaultBaseUrl()
                .WithJsonResponse(ResponseCode.Ok, @"{""Data"": ""expected data""}")
                .CreateFakeClusterClient();
            var http = CreateHttp(clusterClient);
            
            var messageDto = await http.GetAsync<MessageDto>("/some-resource");
            
            messageDto.Should().BeEquivalentTo(expectedMessageDto);
        }

        private static HttpRequestsFactory CreateHttp(IClusterClient clusterClient) => new(
            new RequestSendingOptions(),
            new AuthenticationOptions("apiKey", new FakeAuthProvider()),
            clusterClient,
            new RequestBodySerializer(),
            new TestLogger()
        );

        private class MessageDto
        {
            [UsedImplicitly] 
            public string? Data { get; set; }
        }
    }
}