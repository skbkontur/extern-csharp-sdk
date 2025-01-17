using Kontur.Extern.Api.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.Contents
{
    internal class BytesContent : IHttpContent
    {
        private readonly byte[] bytes;
        private readonly int offset;
        private readonly int length;

        public BytesContent(byte[] bytes, int offset, int length)
        {
            this.bytes = bytes;
            this.offset = offset;
            this.length = length;
        }

        public long? Length => bytes.Length;

        public Request Apply(Request request, IJsonSerializer serializer) => request.WithContent(bytes, offset, length);
    }
}