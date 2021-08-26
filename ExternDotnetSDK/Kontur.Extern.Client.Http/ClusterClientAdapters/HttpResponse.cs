using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Kontur.Extern.Client.Http.Constants;
using Kontur.Extern.Client.Http.Exceptions;
using Kontur.Extern.Client.Http.Models.Headers;
using Kontur.Extern.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Client.Http.ClusterClientAdapters
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
                return await ToArrayAsync(response.Stream).ConfigureAwait(false);

            throw Errors.ResponseHasToHaveBody(request.ToString(true, false));
        }
        
        public async ValueTask<ArraySegment<byte>> GetBytesSegmentAsync()
        {
            if (response.HasContent)
                return response.Content.ToArraySegment();
            
            if (response.HasStream)
            {
                var bytes = await ToArrayAsync(response.Stream).ConfigureAwait(false);
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

            return DeserializeBody<TResponseMessage>();
        }

        public async ValueTask<TResponseMessage?> TryGetMessageAsync<TResponseMessage>()
        {
            var contentType = ContentType;
            if (typeof (TResponseMessage) == typeof (string) && contentType.IsPlainText && (response.HasContent || response.HasStream))
                return (TResponseMessage)(object)await GetStringAsync().ConfigureAwait(false);

            if (!contentType.IsJson)
                return default;

            if (!response.HasStream && !response.HasContent)
                return default;
            
            return DeserializeBody<TResponseMessage>();
        }

        public HttpStatus Status => new(response.Code);

        private TResponseMessage DeserializeBody<TResponseMessage>()
        {
            return response.HasStream
                ? serializer.DeserializeFromJson<TResponseMessage>(response.Stream)
                : serializer.DeserializeFromJson<TResponseMessage>(response.Content.ToArraySegment());
        }

        private static async ValueTask<byte[]> ToArrayAsync(Stream stream)
        {
            if (stream is MemoryStream memoryStream)
                return memoryStream.ToArray();
                
            var count = stream.Length - stream.Position;
            if (count == 0)
                return Array.Empty<byte>();
            // todo: apply an uninitialized array creation
            //byte[] copy = GC.AllocateUninitializedArray<byte>(count);
            var copy = new byte[count];
            await stream.ReadAsync(copy, 0, copy.Length).ConfigureAwait(false);
            return copy;
        }
    }
}