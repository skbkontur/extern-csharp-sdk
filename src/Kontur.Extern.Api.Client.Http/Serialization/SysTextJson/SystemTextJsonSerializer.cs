using System;
using System.Buffers;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Buffers;

namespace Kontur.Extern.Api.Client.Http.Serialization.SysTextJson
{
    internal class SystemTextJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerOptions serializerOptions;
        private readonly JsonSerializerOptions writeIndentedOptions;
        private readonly ArrayPool<byte> bytesPool;
        private readonly JsonWriterOptions writerOptions;

        public SystemTextJsonSerializer(JsonSerializerOptions options, bool ignoreIndentation)
        {
            serializerOptions = options;
            writeIndentedOptions = ignoreIndentation
                ? serializerOptions
                : new JsonSerializerOptions(serializerOptions)
                {
                    WriteIndented = true
                };

            writerOptions = new JsonWriterOptions
            {
                Encoder = serializerOptions.Encoder,
                Indented = serializerOptions.WriteIndented,
                // todo: benchmark with this option to find out how important it is
                //SkipValidation = true 
            };
            
            bytesPool = ArrayPool<byte>.Shared;
        }

        public ArraySegment<byte> SerializeToJsonBytes<T>(T body)
        {
            using var bufferWriter = new PooledByteBufferWriter(bytesPool, serializerOptions.DefaultBufferSize);
            using var writer = new Utf8JsonWriter(bufferWriter, writerOptions);
            
            JsonSerializer.Serialize(writer, body, serializerOptions);

            var bytes = bufferWriter.WrittenMemory.ToArray();
            return new ArraySegment<byte>(bytes);
        }

        public async ValueTask SerializeToJsonStreamAsync<T>(T body, Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                // ReSharper disable once UseAwaitUsing
                using var writer = new Utf8JsonWriter(memoryStream, writerOptions);
                JsonSerializer.Serialize(writer, body, serializerOptions);
            }

            await JsonSerializer.SerializeAsync(stream, body, serializerOptions).ConfigureAwait(false);
        }
        
        public string SerializeToIndentedString<T>(T instance) => 
            JsonSerializer.Serialize(instance, writeIndentedOptions);

        public async ValueTask<DeserializationResult<TResult>> TryDeserializeAsync<TResult>(Stream stream)
        {
            try
            {
                var result = await JsonSerializer.DeserializeAsync<TResult>(stream, serializerOptions).ConfigureAwait(false);
                return DeserializationResult<TResult>.Success(result);
            }
            catch (Exception ex)
            {
                return DeserializationResult<TResult>.Failed(ex);
            }
        }

        public DeserializationResult<TResult> TryDeserialize<TResult>(in ArraySegment<byte> arraySegment)
        {
            try
            {
                var reader = new Utf8JsonReader(arraySegment.AsSpan());
                var result = JsonSerializer.Deserialize<TResult>(ref reader, serializerOptions);
                return DeserializationResult<TResult>.Success(result);
            }
            catch (Exception ex)
            {
                return DeserializationResult<TResult>.Failed(ex);
            }
        }

        public DeserializationResult<TResult> TryDeserialize<TResult>(string jsonText)
        {
            try
            {
                var result = JsonSerializer.Deserialize<TResult>(jsonText, serializerOptions);
                return DeserializationResult<TResult>.Success(result);
            }
            catch (Exception ex)
            {
                return DeserializationResult<TResult>.Failed(ex);
            }
        }
    }
}