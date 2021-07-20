using System;
using System.IO;
using System.Text;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.HttpLevel.Constants;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core.Model;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Client.HttpLevel.ClusterClientAdapters
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

        public byte[] GetBytes()
        {
            if (response.HasContent)
                return response.Content.ToArray();
            
            if (response.HasStream)
                return ToArray(response.Stream);

            throw Errors.ResponseHasToHaveBody(request.ToString(true, false));

            static byte[] ToArray(Stream stream)
            {
                if (stream is MemoryStream memoryStream)
                    return memoryStream.ToArray();
                
                var count = stream.Length - stream.Position;
                if (count == 0)
                    return Array.Empty<byte>();
                // todo: apply an uninitialized array creation
                //byte[] copy = GC.AllocateUninitializedArray<byte>(count);
                var copy = new byte[count];
                // todo: read asynchronously (this should be rare case) and use ValueTask to avoid allocation in the other cases
                stream.Read(copy, 0, copy.Length);
                return copy;
            }
        }

        public string GetString()
        {
            if (response.HasContent)
                return response.Content.ToString(DefaultEncoding);

            return response.HasStream 
                ? GetStringFromStream(response.Stream) 
                : throw Errors.ResponseHasToHaveBody(request.ToString(true, false));

            static string GetStringFromStream(Stream stream)
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
                // todo: read asynchronously (this should be rare case) and use ValueTask to avoid allocation in the other cases
                return streamReader.ReadToEnd();
            }
        }

        public TResponseMessage GetMessage<TResponseMessage>()
        {
            if (response.Headers.ContentType?.StartsWith(ContentTypes.Json) != true)
                throw Errors.ResponseHasUnexpectedContentType(request.ToString(true, false), response, ContentTypes.Json);
            
            if (!response.HasStream && !response.HasContent)
                throw Errors.ResponseHasToHaveBody(request.ToString(true, false));

            var stream = response.HasStream 
                ? response.Stream 
                : response.Content.ToMemoryStream();
            return serializer.DeserializeFromJson<TResponseMessage>(stream);
        }

        public bool TryGetMessage<TResponseMessage>(out TResponseMessage responseMessage)
        {
            responseMessage = default;
            if (response.Headers.ContentType?.StartsWith(ContentTypes.Json) != true)
                return false;
            
            if (!response.HasStream && !response.HasContent)
                return false;
            
            var stream = response.HasStream 
                ? response.Stream 
                : response.Content.ToMemoryStream();
            
            responseMessage = serializer.DeserializeFromJson<TResponseMessage>(stream);
            return true;
        }

        public HttpStatus Status => new(response.Code);
    }
}