using System;
using System.Buffers;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kontur.Extern.Client.Http.Serialization
{
    public class SystemTextJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerOptions serializerOptions;
        private readonly JsonSerializerOptions writeIndentedOptions;
        private readonly ArrayPool<byte> bytesPool;
        private readonly JsonWriterOptions writerOptions;

        public SystemTextJsonSerializer(bool ignoreIndentation = false)
            : this(null, Array.Empty<JsonConverter>(), ignoreIndentation)
        {
        }

        public SystemTextJsonSerializer(JsonNamingPolicy? namingPolicy, JsonConverter[] jsonConverters, bool ignoreIndentation)
        {
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = namingPolicy,
                IgnoreNullValues = true,
                IncludeFields = true
            };
            foreach (var converter in jsonConverters)
            {
                serializerOptions.Converters.Add(converter);
            }

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
        
        public void SerializeToJsonStream<T>(T body, Stream stream)
        {
            using var writer = new Utf8JsonWriter(stream, writerOptions);
            JsonSerializer.Serialize(writer, body, serializerOptions);
        }

        public TResult DeserializeFromJson<TResult>(Stream stream)
        {
            int streamLength;
            checked
            {
                streamLength = (int) stream.Length;
            }

            var bytes = bytesPool.Rent(streamLength);
            try
            {
                stream.Read(bytes, 0, bytes.Length);
                // todo: allow to return nullable TResult
                return DeserializeFromSpan<TResult>(bytes.AsSpan().Slice(0, streamLength));
            }
            finally
            {
                bytesPool.Return(bytes);
            }
        }

        public TResult DeserializeFromJson<TResult>(ArraySegment<byte> arraySegment) => 
            DeserializeFromSpan<TResult>(arraySegment.AsSpan());

        public TResult DeserializeFromJson<TResult>(string jsonText)
        {
            // todo: allow to return nullable TResult
            return JsonSerializer.Deserialize<TResult>(jsonText, serializerOptions)!;
        }

        public string SerializeToIndentedString<T>(T instance) => 
            JsonSerializer.Serialize(instance, writeIndentedOptions);

        private TResult DeserializeFromSpan<TResult>(ReadOnlySpan<byte> span)
        {
            var reader = new Utf8JsonReader(span);
            // todo: allow to return nullable TResult
            return JsonSerializer.Deserialize<TResult>(ref reader, serializerOptions)!;
        }
    }
}