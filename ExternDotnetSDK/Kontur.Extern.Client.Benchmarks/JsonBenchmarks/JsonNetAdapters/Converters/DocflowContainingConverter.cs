#nullable enable
using System;
using System.IO;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions;
using Kontur.Extern.Client.Model.Docflows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters.Converters
{
    internal class DocflowContainingConverter<T> : JsonConverter<T>
        where T : class, IDocflowDto, new()
     {
         private const string TypePropName = "type";
         private const string DescriptionPropName = "description";

         public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
         {
             var serializerWithoutTheConverter = serializer.RemoveConverter(this);
             serializerWithoutTheConverter.Serialize(writer, value);
         }

         public override T ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
         {
             if (reader.TokenType == JsonToken.Null)
                 return null!;

             var jObject = JObject.Load(reader);
             
             var docflowType = TryGetTypeValue(jObject);
             var description = TryGetDocflowDescription(jObject, docflowType, serializer);
             jObject.Remove(DescriptionPropName);

             var result = new T();
             serializer.Populate(jObject.CreateReader(), result);
             if (description != null)
             {
                 result.Description = description;
             }

             return result;

             static DocflowType? TryGetTypeValue(JObject jObject)
             {
                 if (!jObject.TryGetValue(TypePropName, StringComparison.Ordinal, out var typePropValueToken))
                     return null;

                 if (typePropValueToken is not JValue {Type: JTokenType.String} typeValueToken)
                     return null;

                 return typeValueToken.Value?.ToString()!;
             }

             static DocflowDescription? TryGetDocflowDescription(JObject jObject, in DocflowType? docflowType, JsonSerializer serializer)
             {
                 if (docflowType == null)
                     return null;

                 if (!jObject.TryGetValue(DescriptionPropName, out var descriptionJObject) || !descriptionJObject.HasValues)
                     return null;

                 var descriptionType = DocflowDescriptionTypes.TryGetDescriptionType(docflowType.Value) ??
                                       throw new JsonSerializationException($"No docflow description created for type {docflowType}.");

                 using var stringReader = new StringReader(descriptionJObject.ToString());
                 return (DocflowDescription?) serializer.Deserialize(stringReader, descriptionType);
             }
         }
     }
}