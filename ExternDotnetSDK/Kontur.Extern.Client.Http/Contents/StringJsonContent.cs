using Kontur.Extern.Client.Http.Constants;
using Kontur.Extern.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Http.Contents
{
    internal class StringJsonContent : IHttpContent
    {
        private readonly string json;

        public StringJsonContent(string json) => this.json = json;

        public Request Apply(Request request, IJsonSerializer serializer) => 
            request.WithContent(json).WithContentTypeHeader(ContentTypes.Json);
    }
}