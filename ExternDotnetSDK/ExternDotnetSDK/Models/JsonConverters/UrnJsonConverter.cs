using System;
using Kontur.Extern.Client.Models.Common;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.JsonConverters
{
    public class UrnJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            serializer.Serialize(writer, ((Urn)value).ToString());

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var urnParts = reader.Value.ToString().Split(':');
            return new Urn(urnParts[1], urnParts[2]);
        }

        public override bool CanConvert(Type objectType) => typeof (Urn).IsAssignableFrom(objectType);
    }
}