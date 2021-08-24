using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Http.Serialization
{
    public class JsonSerializer : IJsonSerializer
    {
        private static  readonly UTF8Encoding Utf8NoBom = new(false, true);
        private readonly Newtonsoft.Json.JsonSerializer jsonSerializer;
        private readonly Newtonsoft.Json.JsonSerializer indentedJsonSerializer;

        public JsonSerializer()
        {
            jsonSerializer = new Newtonsoft.Json.JsonSerializer();
            indentedJsonSerializer = new Newtonsoft.Json.JsonSerializer
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
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