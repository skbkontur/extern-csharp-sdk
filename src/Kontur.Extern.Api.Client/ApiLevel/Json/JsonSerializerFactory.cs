using System.Text.Json;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.EnumLikeTypes;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Tasks;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.NamingPolicies;

namespace Kontur.Extern.Api.Client.ApiLevel.Json
{
    public static class JsonSerializerFactory
    {
        public static IJsonSerializer CreateJsonSerializer(bool ignoreIndentation = false, bool ignoreNullValues = true)
        {
            var namingPolicy = new KebabCaseNamingPolicy();
            return new SystemTextJsonSerializerFactory()
                .WithNamingPolicy(namingPolicy)
                .AddConverter(new UrnJsonConverter())
                .AddConverter(new StringBasedValueTypesConverter())
                .AddConverter(new DocflowConverter())
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