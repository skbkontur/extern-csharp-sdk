using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Kontur.Extern.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters.Bytes;
using Kontur.Extern.Client.Http.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters
{
    internal class JsonNetSerializer : IJsonSerializer
    {
        private static  readonly UTF8Encoding Utf8NoBom = new(false, true);
        private readonly JsonSerializer jsonSerializer;
        private readonly JsonSerializer indentedJsonSerializer;

        public JsonNetSerializer(NamingStrategy namingStrategy, JsonConverter[] converters, bool ignoreIndentation)
            : this(converters, namingStrategy ?? throw new ArgumentNullException(nameof(namingStrategy)), ignoreIndentation)
        {
        }
        
        private JsonNetSerializer(JsonConverter[] converters, NamingStrategy? namingStrategy, bool ignoreIndentation)
        {
            jsonSerializer = new JsonSerializer();

            foreach (var converter in converters)
            {
                jsonSerializer.Converters.Add(converter);
            }
            
            if (namingStrategy != null)
            {
                jsonSerializer.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = namingStrategy
                };
            }

            if (ignoreIndentation)
            {
                indentedJsonSerializer = jsonSerializer;
            }
            else
            {
                indentedJsonSerializer = new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                
                foreach (var converter in converters)
                {
                    indentedJsonSerializer.Converters.Add(converter);
                }
                
                if (namingStrategy != null)
                {
                    indentedJsonSerializer.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = namingStrategy
                    };
                }

                indentedJsonSerializer.Formatting = Formatting.Indented;
            }
        }

        public ArraySegment<byte> SerializeToJsonBytes<T>(T body)
        {
            using var stream = new MemoryStream();
            SerializeToJsonStreamAsync(body, stream);
            var bytes = stream.ToArray();
            return new ArraySegment<byte>(bytes);
        }

        public ValueTask SerializeToJsonStreamAsync<T>(T body, Stream stream)
        {
            using var streamWriter = new StreamWriter(stream, Utf8NoBom, 1024, true);
            jsonSerializer.Serialize(streamWriter, body);
            return new();
        }

        public ValueTask<DeserializationResult<TResult>> TryDeserializeAsync<TResult>(Stream stream)
        {
            using var streamReader = new StreamReader(stream, Utf8NoBom);
            return new(TryDeserialize<TResult>(streamReader));
        }

        public DeserializationResult<TResult> TryDeserialize<TResult>(in ArraySegment<byte> arraySegment)
        {
            using var streamReader = new StreamReader(arraySegment.AsMemoryStream(), Utf8NoBom);
            return TryDeserialize<TResult>(streamReader);
        }

        public DeserializationResult<TResult> TryDeserialize<TResult>(string jsonText)
        {
            using var stringReader = new StringReader(jsonText);
            return TryDeserialize<TResult>(stringReader);
        }

        public string SerializeToIndentedString<T>(T instance)
        {
            using var stringWriter = new StringWriter();
            indentedJsonSerializer.Serialize(stringWriter, instance);
            return stringWriter.ToString();
        }

        private DeserializationResult<TResult> TryDeserialize<TResult>(TextReader streamReader)
        {
            try
            {
                var result = jsonSerializer.Deserialize<TResult>(new JsonTextReader(streamReader));
                return DeserializationResult<TResult>.Success(result);
            }
            catch (Exception ex)
            {
                return DeserializationResult<TResult>.Failed(ex);
            }
        }
    }
}