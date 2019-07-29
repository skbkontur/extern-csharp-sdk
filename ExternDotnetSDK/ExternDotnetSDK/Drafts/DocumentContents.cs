using ExternDotnetSDK.Drafts.Requests;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocumentContents
    {
        public string Base64Content { get; set; }
        public string Signature { get; set; }
        public DocumentDescriptionRequestDto Description { get; set; }
    }
}