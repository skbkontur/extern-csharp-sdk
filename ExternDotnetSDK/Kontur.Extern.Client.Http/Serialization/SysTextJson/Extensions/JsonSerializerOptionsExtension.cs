using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kontur.Extern.Client.ApiLevel.Json.SysTextJsonExtensions
{
    public static class JsonSerializerOptionsExtension
    {
        public static JsonSerializerOptions RemoveConverterOf(this JsonSerializerOptions options, Type converterType)
        {
            var converterIndex = FindConverterIndex(options, converterType);
            if (converterIndex < 0)
                return options;
            
            var clone = new JsonSerializerOptions(options);
            clone.Converters.RemoveAt(converterIndex);
            return clone;

            int FindConverterIndex(JsonSerializerOptions jsonSerializerOptions, Type type)
            {
                for (var i = 0; i < jsonSerializerOptions.Converters.Count; i++)
                {
                    if (type.IsInstanceOfType(jsonSerializerOptions.Converters[i]))
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
        
        public static JsonSerializerOptions RemoveConverter(this JsonSerializerOptions options, JsonConverter converter)
        {
            var converterIndex = options.Converters.IndexOf(converter);
            if (converterIndex < 0)
                return options;
            var clone = new JsonSerializerOptions(options);
            clone.Converters.RemoveAt(converterIndex);
            return clone;
        }
    }
}