using System.Text;
using Kontur.Extern.Api.Client.Http.Constants;
using Kontur.Extern.Api.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.Contents
{
    internal class StringJsonContent : IHttpContent
    {
        private readonly Content json;

        public StringJsonContent(string json) => 
            this.json = new Content(Encoding.UTF8.GetBytes(json));

        public long? Length => json.Length;

        public Request Apply(Request request, IJsonSerializer serializer) => 
            request.WithContent(json).WithContentTypeHeader(ContentTypes.Json);
    }
}