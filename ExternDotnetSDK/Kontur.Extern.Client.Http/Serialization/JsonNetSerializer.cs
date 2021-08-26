using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kontur.Extern.Client.Http.Serialization
{
    public class JsonNetSerializer : IJsonSerializer
    {
        private static  readonly UTF8Encoding Utf8NoBom = new(false, true);
        private readonly JsonSerializer jsonSerializer;
        private readonly JsonSerializer indentedJsonSerializer;

        public JsonNetSerializer()
            : this(Array.Empty<JsonConverter>(), null, false)
        {
        }

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

        public string SerializeToIndentedString<T>(T instance)
        {
            using var stringWriter = new StringWriter();
            indentedJsonSerializer.Serialize(stringWriter, instance);
            return stringWriter.ToString();
        }
    }
}