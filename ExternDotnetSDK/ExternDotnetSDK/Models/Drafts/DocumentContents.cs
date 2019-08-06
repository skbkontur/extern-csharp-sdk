using ExternDotnetSDK.Models.Drafts.Requests;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocumentContents
    {
        public string Base64Content { get; set; }
        public string Signature { get; set; }
        public DocumentDescriptionRequestDto Description { get; set; }
    }
}