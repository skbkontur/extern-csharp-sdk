using System;
using System.Collections.Generic;
using ExternDotnetSDK.Common;
using Newtonsoft.Json;

namespace ExternDotnetSDK.JsonConverters
{
    internal class ExternSdkJsonConverter : JsonConverter
    {
        private static Dictionary<Type, Func<JsonReader, Type, object, JsonSerializer, object>> readMethods
            = new Dictionary<Type, Func<JsonReader, Type, object, JsonSerializer, object>>
            {
                [typeof (Urn)] = (reader, type, value, serializer) =>
                {
                    var urnParts = reader.Value.ToString().Split(':');
                    return new Urn(urnParts[1], urnParts[2]);
                },
            };

        private static Dictionary<Type, Func<Type, bool>> convertMethods = new Dictionary<Type, Func<Type, bool>>
        {
            [typeof(Urn)] = type => typeof (Urn).IsAssignableFrom(type),
        };

        private static Dictionary<Type, Action<JsonWriter, object, JsonSerializer>> writeMethods
            = new Dictionary<Type, Action<JsonWriter, object, JsonSerializer>>
            {
                [typeof(Urn)] = (writer, value, serializer) => serializer.Serialize(writer, ((Urn)value).ToString()),
            };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType();
            if (writeMethods.ContainsKey(type))
                writeMethods[type](writer, value, serializer);
            else throw new KeyNotFoundException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (readMethods.ContainsKey(objectType))
                return readMethods[objectType](reader, objectType, existingValue, serializer);
            throw new KeyNotFoundException();
        }

        public override bool CanConvert(Type objectType)
        {
            return convertMethods.ContainsKey(objectType) && convertMethods[objectType](objectType);
        }
    }
}