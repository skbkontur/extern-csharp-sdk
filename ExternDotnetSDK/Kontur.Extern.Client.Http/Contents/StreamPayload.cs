using System.IO;
using Kontur.Extern.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Http.Contents
{
    public class StreamPayload : IHttpContent
    {
        private readonly Stream stream;

        public StreamPayload(Stream stream) => this.stream = stream;

        public long? Length => stream.CanSeek ? stream.Length : null;

        public Request Apply(Request request, IJsonSerializer serializer) => request.WithContent(stream);
    }
}