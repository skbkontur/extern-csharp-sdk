using KeApiClientOpenSdk.Models.Drafts.Requests;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocumentContents
    {
        public string Base64Content { get; set; }
        public string Signature { get; set; }
        public DocumentDescriptionRequestDto Description { get; set; }
    }
}