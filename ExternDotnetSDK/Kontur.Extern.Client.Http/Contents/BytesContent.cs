using Kontur.Extern.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Http.Contents
{
    internal class BytesContent : IHttpContent
    {
        private readonly byte[] bytes;

        public BytesContent(byte[] bytes) => this.bytes = bytes;

        public long? Length => bytes.Length;

        public Request Apply(Request request, IJsonSerializer serializer) => request.WithContent(bytes);
    }
}