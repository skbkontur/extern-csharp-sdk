using System;
using System.Collections.Generic;
using ExternDotnetSDK.Common;
using Newtonsoft.Json;

namespace ExternDotnetSDK.JsonConverters
{
    internal class ExternSdkJsonConverter : JsonConverter
    {
        private static readonly Dictionary<Type, Func<JsonReader, Type, object, JsonSerializer, object>> ReadMethods
            = new Dictionary<Type, Func<JsonReader, Type, object, JsonSerializer, object>>
            {
                [typeof (Urn)] = (reader, type, value, serializer) =>
                {
                    var urnParts = reader.Value.ToString().Split(':');
                    return new Urn(urnParts[1], urnParts[2]);
                }
            };

        private static readonly Dictionary<Type, Func<Type, bool>> ConvertMethods = new Dictionary<Type, Func<Type, bool>>
        {
            [typeof(Urn)] = type => typeof (Urn).IsAssignableFrom(type)
        };

        private static readonly Dictionary<Type, Action<JsonWriter, object, JsonSerializer>> WriteMethods
            = new Dictionary<Type, Action<JsonWriter, object, JsonSerializer>>
            {
                [typeof(Urn)] = (writer, value, serializer) => serializer.Serialize(writer, ((Urn)value).ToString())
            };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType();
            if (WriteMethods.ContainsKey(type))
                WriteMethods[type](writer, value, serializer);
            else throw new KeyNotFoundException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            ReadMethods.ContainsKey(objectType)
                ? ReadMethods[objectType](reader, objectType, existingValue, serializer)
                : throw new KeyNotFoundException();

        public override bool CanConvert(Type objectType) =>
            ConvertMethods.ContainsKey(objectType) && ConvertMethods[objectType](objectType);
    }
}