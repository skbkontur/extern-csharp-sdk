using System.Text.Json;
using Kontur.Extern.Client.ApiLevel.Json.Converters;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data.BusinessRegistration;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingPolicies;

namespace Kontur.Extern.Client.ApiLevel.Json
{
    public static class JsonSerializerFactory
    {
        public static IJsonSerializer CreateJsonSerializer(bool ignoreIndentation = false, bool ignoreNullValues = true) =>
            new SystemTextJsonSerializerFactory()
                .WithNamingPolicy(new KebabCaseNamingPolicy())
                .AddConverter(new UrnJsonConverter())
                .AddConverter(new DocflowContainingConverter())
                .AddConverter(new DocflowDocumentDescriptionConverter())
                .AddConverter(new DraftsBuilderMetaConverter())
                .AddConverter(new DraftsBuilderDocumentMetaConverter())
                .SetCustomNamingPolicyForSerializationEnumOf<PaperDocumentsDeliveryType>(JsonNamingPolicy.CamelCase)
                .IgnoreIndentation(ignoreIndentation)
                .IgnoreNullValues(ignoreNullValues)
                .CreateSerializer();
    }
}