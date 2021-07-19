using System.IO;
using Kontur.Extern.Client.HttpLevel.Constants;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.HttpLevel.Contents
{
    public static class ObjectJsonContent
    {
        public static ObjectJsonContent<T> WithMessage<T>(T message) => new(message);
    }

    public class ObjectJsonContent<T> : IHttpContent    
    {
        private readonly T message;

        public ObjectJsonContent(T message) => this.message = message;

        public Request Apply(Request request, IJsonSerializer serializer)
        {
            var memoryStream = new MemoryStream();
            serializer.SerializeToJsonStream(message, memoryStream);
            memoryStream.Position = 0;
            return request.WithContent(new StreamContent(memoryStream)).WithContentTypeHeader(ContentTypes.Json);
        }
    }
}