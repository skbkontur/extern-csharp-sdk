using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.DocumentFiles
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderDocumentFileContents
    {
        public string Base64Content { get; set; }
        public string Base64SignatureContent { get; set; }
        public DraftsBuilderDocumentFileMetaRequest Meta { get; set; }
    }
}