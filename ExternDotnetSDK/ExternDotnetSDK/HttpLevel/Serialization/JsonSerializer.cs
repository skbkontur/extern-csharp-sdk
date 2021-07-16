#nullable enable
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.HttpLevel.Serialization
{
    public class JsonSerializer : IJsonSerializer
    {
        private static  readonly UTF8Encoding Utf8NoBom = new(false, true);
        private readonly Newtonsoft.Json.JsonSerializer jsonSerializer;

        public JsonSerializer() => jsonSerializer = new Newtonsoft.Json.JsonSerializer();

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
    }
}