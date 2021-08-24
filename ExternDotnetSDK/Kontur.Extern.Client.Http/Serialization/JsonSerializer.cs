using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kontur.Extern.Client.Http.Serialization
{
    public class JsonSerializer : IJsonSerializer
    {
        private static  readonly UTF8Encoding Utf8NoBom = new(false, true);
        private readonly Newtonsoft.Json.JsonSerializer jsonSerializer;
        private readonly Newtonsoft.Json.JsonSerializer indentedJsonSerializer;

        public JsonSerializer()
            : this(Array.Empty<JsonConverter>(), null)
        {
        }

        public JsonSerializer(NamingStrategy namingStrategy, JsonConverter[] converters)
            : this(converters, namingStrategy ?? throw new ArgumentNullException(nameof(namingStrategy)))
        {
        }
        
        private JsonSerializer(JsonConverter[] converters, NamingStrategy? namingStrategy)
        {
            jsonSerializer = new Newtonsoft.Json.JsonSerializer();
            indentedJsonSerializer = new Newtonsoft.Json.JsonSerializer
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
            
            foreach (var converter in converters)
            {
                jsonSerializer.Converters.Add(converter);
                indentedJsonSerializer.Converters.Add(converter);
            }
            
            if (namingStrategy != null)
            {
                jsonSerializer.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = namingStrategy
                };
                indentedJsonSerializer.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = namingStrategy
                };
            }
        }

        public void SerializeToJsonStream<T>(T body, Stream stream)
        {
            using var streamWriter = new StreamWriter(stream, Utf8NoBom, 1024, true);
            jsonSerializer.Serialize(streamWriter, body);
        }

        public TResult DeserializeFromJson<TResult>(Stream stream)
        {
            using var streamReader = new StreamReader(stream, Utf8NoBom);
            return jsonSerializer.Deserialize<TResult>(new JsonTextReader(streamReader));
        }

        public TResult DeserializeFromJson<TResult>(string jsonText)
        {
            using var stringReader = new StringReader(jsonText);
            return jsonSerializer.Deserialize<TResult>(new JsonTextReader(stringReader));
        }

        public void SerializeToIndentedString<T>(T instance, StringBuilder stringBuilder)
        {
            using var stringWriter = new StringWriter(stringBuilder);
            indentedJsonSerializer.Serialize(stringWriter, instance);
        }

        public string SerializeToIndentedString<T>(T instance)
        {
            using var stringWriter = new StringWriter();
            indentedJsonSerializer.Serialize(stringWriter, instance);
            return stringWriter.ToString();
        }
    }
}