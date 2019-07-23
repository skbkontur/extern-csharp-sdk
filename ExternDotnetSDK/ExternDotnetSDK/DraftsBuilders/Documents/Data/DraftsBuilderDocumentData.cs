using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.Documents.Data
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public abstract class DraftsBuilderDocumentData
    {
    }
}