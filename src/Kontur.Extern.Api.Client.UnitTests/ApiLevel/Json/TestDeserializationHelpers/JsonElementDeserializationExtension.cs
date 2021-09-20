using System;
using System.Text.Json;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.TestDeserializationHelpers
{
    internal static class JsonElementDeserializationExtension
    {
        public static T? Deserialize<T>(this JsonElement jsonElement, JsonSerializerOptions? options = null)
        {
            var json = jsonElement.GetRawText();
            return JsonSerializer.Deserialize<T>(json, options);
        }
        
        public static object? Deserialize(this JsonElement jsonElement, Type targetType, JsonSerializerOptions? options = null)
        {
            var json = jsonElement.GetRawText();
            return JsonSerializer.Deserialize(json, targetType, options);
        }
    }
}