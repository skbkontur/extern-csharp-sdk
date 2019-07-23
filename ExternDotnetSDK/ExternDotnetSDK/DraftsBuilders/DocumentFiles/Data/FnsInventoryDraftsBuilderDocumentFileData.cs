using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.DocumentFiles.Data
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class FnsInventoryDraftsBuilderDocumentFileData : DraftsBuilderDocumentFileData
    {
    }
}