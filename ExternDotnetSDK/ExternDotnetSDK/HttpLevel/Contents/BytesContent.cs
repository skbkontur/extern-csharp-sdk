using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.HttpLevel.Contents
{
    internal class BytesContent : IHttpContent
    {
        private readonly byte[] bytes;

        public BytesContent(byte[] bytes) => this.bytes = bytes;

        public Request Apply(Request request, IJsonSerializer serializer) => request.WithContent(bytes);
    }
}