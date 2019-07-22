using System;
using ExternDotnetSDK.Common;
using Newtonsoft.Json;

namespace ExternDotnetSDK.JsonConverters
{
    public class UrnConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var urn = (Urn)value;
            serializer.Serialize(writer, urn.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var urnParts = reader.Value.ToString().Split(':');
            return new Urn(urnParts[1], urnParts[2]);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (Urn).IsAssignableFrom(objectType);
        }
    }
}