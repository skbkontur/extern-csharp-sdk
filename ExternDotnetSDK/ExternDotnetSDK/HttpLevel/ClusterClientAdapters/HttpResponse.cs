using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core.Model;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Client.HttpLevel.ClusterClientAdapters
{
    internal class HttpResponse : IHttpResponse
    {
        private readonly Request request;
        private readonly Response response;
        private readonly IRequestBodySerializer serializer;

        public HttpResponse(Request request, Response response, IRequestBodySerializer serializer)
        {
            this.request = request;
            this.response = response;
            this.serializer = serializer;
        }

        public byte[] GetBytes() => response.Content.ToArray();
        
        public string GetString() => response.Content.ToString();

        public TResponseMessage GetMessage<TResponseMessage>()
        {
            if (response.Headers.ContentType != ContentTypes.Json) 
                throw Errors.ResponseHasUnexpectedContentType(request.ToString(true, false), response, ContentTypes.Json);
            
            if (!response.HasStream && !response.HasContent)
                throw Errors.ResponseHasToHaveBody(request.ToString(true, false));

            var stream = response.HasStream 
                ? response.Stream 
                : response.Content.ToMemoryStream();
            return serializer.DeserializeFromJson<TResponseMessage>(stream);
        }
    }
}