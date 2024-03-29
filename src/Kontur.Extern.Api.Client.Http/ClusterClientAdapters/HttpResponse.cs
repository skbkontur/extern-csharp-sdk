using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Common.Streams;
using Kontur.Extern.Api.Client.Http.Constants;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Kontur.Extern.Api.Client.Http.Models.Headers;
using Kontur.Extern.Api.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters
{
    internal class HttpResponse : IHttpResponse
    {
        private static readonly Encoding DefaultEncoding = Encoding.UTF8;
        private readonly Request request;
        private readonly Response response;
        private readonly IJsonSerializer serializer;

        public HttpResponse(Request request, Response response, IJsonSerializer serializer)
        {
            this.request = request;
            this.response = response;
            this.serializer = serializer;
        }

        public bool HasPayload => response.HasContent || response.HasStream;

        public ContentRangeHeaderValue ContentRange =>
            ContentRangeHeaderValue.Parse(response.Headers.ContentRange);
        
        public ContentType ContentType
        {
            get
            {
                var contentTypeHeaderValue = response.Headers.ContentType;
                return contentTypeHeaderValue is null ? default : new ContentType(contentTypeHeaderValue);
            }
        }

        public async ValueTask<byte[]> GetBytesAsync()
        {
            if (response.HasContent)
                return response.Content.ToArray();
            
            if (response.HasStream)
                return await response.Stream.ToArrayAsync().ConfigureAwait(false);

            throw Errors.ResponseHasToHaveBody(request.ToString(true, false));
        }
        
        public async ValueTask<ArraySegment<byte>> GetBytesSegmentAsync()
        {
            if (response.HasContent)
                return response.Content.ToArraySegment();
            
            if (response.HasStream)
            {
                var bytes = await response.Stream.ToArrayAsync().ConfigureAwait(false);
                return new ArraySegment<byte>(bytes);
            }

            throw Errors.ResponseHasToHaveBody(request.ToString(true, false));
        }
        
        public Stream GetStream()
        {
            if (response.HasContent)
                return response.Content.ToMemoryStream();
            
            if (response.HasStream)
                return response.Stream;

            throw Errors.ResponseHasToHaveBody(request.ToString(true, false));
        }

        public async ValueTask<string> GetStringAsync()
        {
            if (response.HasContent)
                return response.Content.ToString(DefaultEncoding);

            return response.HasStream 
                ? await GetStringFromStream(response.Stream).ConfigureAwait(false) 
                : throw Errors.ResponseHasToHaveBody(request.ToString(true, false));

            static async ValueTask<string> GetStringFromStream(Stream stream)
            {
                if (stream is MemoryStream memoryStream)
                {
                    if (!memoryStream.TryGetBuffer(out var buffer))
                    {
                        buffer = new ArraySegment<byte>(memoryStream.ToArray());
                    }

                    return DefaultEncoding.GetString(buffer.Array!, buffer.Offset, buffer.Count);
                }

                using var streamReader = new StreamReader(stream);
                return await streamReader.ReadToEndAsync().ConfigureAwait(false);
            }
        }

        public async ValueTask<TResponseMessage> GetMessageAsync<TResponseMessage>()
        {
            var contentType = ContentType;
            if (typeof (TResponseMessage) == typeof (string) && contentType.IsPlainText)
                return (TResponseMessage) (object) await GetStringAsync().ConfigureAwait(false);
                
            if (!contentType.IsJson)
                throw Errors.ResponseHasUnexpectedContentType(request.ToString(true, false), response, ContentTypes.Json);
            
            if (!response.HasStream && !response.HasContent)
                throw Errors.ResponseHasToHaveBody(request.ToString(true, false));

            return response.HasStream
                ? await serializer.DeserializeAsync<TResponseMessage>(response.Stream).ConfigureAwait(false)
                : serializer.Deserialize<TResponseMessage>(response.Content.ToArraySegment());
        }

        public async ValueTask<TResponseMessage?> TryGetMessageAsync<TResponseMessage>()
        {
            var contentType = ContentType;
            if (typeof (TResponseMessage) == typeof (string) && contentType.IsPlainText && (response.HasContent || response.HasStream))
                return (TResponseMessage) (object) await GetStringAsync().ConfigureAwait(false);

            if (!contentType.IsJson)
                return default;

            if (!response.HasStream && !response.HasContent)
                return default;

            var result = response.HasStream 
                ? await serializer.TryDeserializeAsync<TResponseMessage>(response.Stream).ConfigureAwait(false) 
                : serializer.TryDeserialize<TResponseMessage>(response.Content.ToArraySegment());
            return result.GetResultOrNull();
        }

        public HttpStatus Status => new(response.Code);
    }
}