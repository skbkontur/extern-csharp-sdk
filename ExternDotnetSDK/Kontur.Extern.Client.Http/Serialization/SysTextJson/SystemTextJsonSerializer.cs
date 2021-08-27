using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.Converters;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson
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
        
        public SystemTextJsonSerializer(JsonNamingPolicy? namingPolicy, bool ignoreIndentation = false)
            : this(namingPolicy, Array.Empty<JsonConverter>(), ignoreIndentation)
        {
        }

        public SystemTextJsonSerializer(JsonNamingPolicy? namingPolicy, JsonConverter[] jsonConverters, bool ignoreIndentation)
        {
            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowRange(UnicodeRanges.BasicLatin);
            encoderSettings.AllowRange(UnicodeRanges.Cyrillic);
            encoderSettings.AllowRange(UnicodeRanges.CyrillicSupplement);
            encoderSettings.AllowRange(UnicodeRanges.CyrillicExtendedA);
            encoderSettings.AllowRange(UnicodeRanges.CyrillicExtendedB);
            encoderSettings.AllowRange(UnicodeRanges.CyrillicExtendedC);

            serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = namingPolicy,
                IgnoreNullValues = true,
                IncludeFields = true,
                Encoder = JavaScriptEncoder.Create(encoderSettings),
            };
            AddConverters(serializerOptions.Converters, namingPolicy, jsonConverters);

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

            static void AddConverters(ICollection<JsonConverter> converters, JsonNamingPolicy? namingPolicy, JsonConverter[] jsonConverters)
            {
                var httpStatusCodeConverterAdded = false;
                var jsonStringEnumConverterAdded = false;
                foreach (var converter in jsonConverters)
                {
                    converters.Add(converter);
                    if (converter is JsonStringEnumConverter)
                    {
                        jsonStringEnumConverterAdded = true;
                    }

                    if (converter is HttpStatusCodeConverter)
                    {
                        httpStatusCodeConverterAdded = true;
                    }
                }

                if (!httpStatusCodeConverterAdded)
                {
                    converters.Add(new HttpStatusCodeConverter(namingPolicy));
                }

                if (!jsonStringEnumConverterAdded)
                {
                    converters.Add(new JsonStringEnumConverter(namingPolicy));
                }
                
                converters.Add(new DateOnlyConverter());
            }
        }

        public ArraySegment<byte> SerializeToJsonBytes<T>(T body)
        {
            // todo: Try to optimize it by port IBufferWriter
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, writerOptions);
            JsonSerializer.Serialize(writer, body, serializerOptions);
            var bytes = stream.ToArray();
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

        public ValueTask<TResult?> DeserializeAsync<TResult>(Stream stream) => 
            JsonSerializer.DeserializeAsync<TResult>(stream, serializerOptions);

        public TResult? Deserialize<TResult>(ArraySegment<byte> arraySegment) => 
            DeserializeFromSpan<TResult>(arraySegment.AsSpan());

        public TResult Deserialize<TResult>(string jsonText)
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