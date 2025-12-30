using System;
using System.Text.Json;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.EnumLikeTypes;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Tasks;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.NamingPolicies;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss.Enums;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.ApiLevel.Json
{
    public static class JsonSerializerFactory
    {
        public static IJsonSerializer CreateJsonSerializer(ILog log, bool ignoreIndentation = false, bool ignoreNullValues = true, Func<SystemTextJsonSerializerFactory>? setupSerializer = null)
        {
            var namingPolicy = new KebabCaseNamingPolicy();
            var factory = new SystemTextJsonSerializerFactory();
            if (setupSerializer is not null)
                factory = setupSerializer();

            return factory.WithNamingPolicy(namingPolicy)
                .AddConverter(new UrnJsonConverter())
                .AddConverter(new StringBasedValueTypesConverter())
                .AddConverter(new DocflowConverter())
                .AddConverter(new DocflowDocumentDescriptionConverter())
                .AddConverter(new DraftsBuilderMetaConverter())
                .AddConverter(new DraftsBuilderDocumentMetaConverter())
                .AddConverter(new DraftsBuilderDocumentFileMetaConverter())
                .AddConverter(new ApiTaskResultJsonConverter(namingPolicy))
                .AddConverter(new WriteAbstractTypeValueAsConcreteTypeValueConverter())
                .AddConverter(new ControlUnitTypesConverter(log))
                .AddConverter(new HandbookPageConverter())
                .SetCustomNamingPolicyForSerializationEnumOf<PaperDocumentsDeliveryType>(JsonNamingPolicy.CamelCase)
                .SetCustomNamingPolicyForSerializationEnumOf<FssStageStatus>(null)
                .SetCustomNamingPolicyForSerializationEnumOf<FssStageType>(null)
                .IgnoreIndentation(ignoreIndentation)
                .IgnoreNullValues(ignoreNullValues)
                .CreateSerializer();
        }
    }
}