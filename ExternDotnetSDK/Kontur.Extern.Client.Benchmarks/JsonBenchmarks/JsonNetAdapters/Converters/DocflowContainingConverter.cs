#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.Model.Docflows;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Docflows.Descriptions;
using Kontur.Extern.Client.Models.Docflows.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters.Converters
{
    internal class DocflowContainingConverter<T> : JsonConverter<T>
        where T : IDocflow
     {
         private const string TypePropName = "type";
         private const string DescriptionPropName = "description";
         private const string DocumentsPropName = "documents";

         public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
         {
             var serializerWithoutTheConverter = serializer.RemoveConverter(this);
             serializerWithoutTheConverter.Serialize(writer, value);
         }

         public override T ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
         {
             if (reader.TokenType == JsonToken.Null)
                 return default!;

             var jObject = JObject.Load(reader);
             
             var docflowType = TryGetTypeValue(jObject);
             var description = TryGetDocflowDescription(jObject, docflowType, serializer);
             jObject.Remove(DescriptionPropName);
             jObject.Remove(DocumentsPropName);

             var result = new Docflow();
             serializer.Populate(jObject.CreateReader(), result);
             if (description != null)
             {
                 result.Description = description;
             }

             return (T) (object) result;

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

         [PublicAPI]
         private class Docflow : IDocflowWithDocuments
         {
             public Guid Id { get; set; }
             public Guid OrganizationId { get; set; }
             public DocflowType Type { get; set; } = null!;
             public Urn Status { get; set; } = null!;
             public Urn SuccessState { get; set; } = null!;
             public List<Document> Documents { get; set; } = null!;
             public List<Link> Links { get; set; } = null!;
             public DateTime SendDateTime { get; set; }
             public DateTime? LastChangeDateTime { get; set; }
             public DocflowDescription Description { get; set; } = null!;

             public bool IsEmpty { get; }
         }
     }
}