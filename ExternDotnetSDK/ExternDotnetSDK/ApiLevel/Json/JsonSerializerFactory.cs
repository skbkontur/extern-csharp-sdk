using System.Text.Json;
using Kontur.Extern.Client.ApiLevel.Json.Converters;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Tasks;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingPolicies;
using Kontur.Extern.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration;

namespace Kontur.Extern.Client.ApiLevel.Json
{
    public static class JsonSerializerFactory
    {
        public static IJsonSerializer CreateJsonSerializer(bool ignoreIndentation = false, bool ignoreNullValues = true)
        {
            var namingPolicy = new KebabCaseNamingPolicy();
            return new SystemTextJsonSerializerFactory()
                .WithNamingPolicy(namingPolicy)
                .AddConverter(new UrnJsonConverter())
                .AddConverter(new DocflowContainingConverter())
                .AddConverter(new DocflowDocumentDescriptionConverter())
                .AddConverter(new DraftsBuilderMetaConverter())
                .AddConverter(new DraftsBuilderDocumentMetaConverter())
                .AddConverter(new DraftsBuilderDocumentFileMetaConverter())
                .AddConverter(new ApiTaskResultJsonConverter(namingPolicy))
                .SetCustomNamingPolicyForSerializationEnumOf<PaperDocumentsDeliveryType>(JsonNamingPolicy.CamelCase)
                .IgnoreIndentation(ignoreIndentation)
                .IgnoreNullValues(ignoreNullValues)
                .CreateSerializer();
        }
    }
}