using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.DocumentFiles.Data
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public abstract class DraftsBuilderDocumentFileData
    {
    }
}