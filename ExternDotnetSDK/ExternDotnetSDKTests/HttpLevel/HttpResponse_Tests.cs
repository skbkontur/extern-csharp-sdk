using System;
using System.IO;
using System.Text;
using FluentAssertions;
using JetBrains.Annotations;
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
        public void GetMessage_should_deserialize_response_stream_to_DTO()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = new HttpResponse(
                DummyRequest,
                new Response(
                    ResponseCode.Ok,
                    stream: ToStream(json),
                    headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json)),
                new RequestBodySerializer()
            );

            var dto = httpResponse.GetMessage<Dto>();
            
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Test]
        public void GetMessage_should_deserialize_response_content_to_DTO()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = new HttpResponse(
                DummyRequest,
                new Response(
                    ResponseCode.Ok,
                    ToContent(json),
                    new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json)),
                new RequestBodySerializer()
            );

            var dto = httpResponse.GetMessage<Dto>();
            
            dto.Should().BeEquivalentTo(expectedDto);
        }

        private static Request DummyRequest => Request.Get(new Uri("/dummy", UriKind.Relative));

        private static MemoryStream ToStream(string json) => new(Encoding.UTF8.GetBytes(json));
        
        private static Content ToContent(string json) => new(Encoding.UTF8.GetBytes(json));

        private class Dto
        {
            [UsedImplicitly]
            public string Data { get; set; }
        }
    }
}