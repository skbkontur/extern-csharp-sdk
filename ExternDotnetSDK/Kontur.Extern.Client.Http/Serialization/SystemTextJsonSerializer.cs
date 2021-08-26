using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kontur.Extern.Client.Http.Serialization
{
    public class SystemTextJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerOptions serializerOptions;

        public SystemTextJsonSerializer(JsonNamingPolicy? namingPolicy, JsonConverter[] jsonConverters)
        {
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = namingPolicy,
                IgnoreNullValues = true,
                IncludeFields = true
            };
            foreach (var converter in jsonConverters)
            {
                serializerOptions.Converters.Add(converter);
            }
        }
        
        public void SerializeToJsonStream<T>(T body, Stream stream)
        {
            throw new NotImplementedException();
        }

        public TResult DeserializeFromJson<TResult>(Stream stream)
        {
            throw new NotImplementedException();
        }

        public TResult DeserializeFromJson<TResult>(string jsonText)
        {
            // todo: allow to return nullable TResult
            return System.Text.Json.JsonSerializer.Deserialize<TResult>(jsonText, serializerOptions)!;
        }

        public string SerializeToIndentedString<T>(T instance) => 
            System.Text.Json.JsonSerializer.Serialize(instance);

        public void SerializeToIndentedString<T>(T instance, StringBuilder stringBuilder)
        {
            throw new NotImplementedException();
        }
    }
}