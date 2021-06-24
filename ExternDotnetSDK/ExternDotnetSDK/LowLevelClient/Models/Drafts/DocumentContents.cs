using Kontur.Extern.Client.Models.Drafts.Requests;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocumentContents
    {
        public string Base64Content { get; set; }
        public string Signature { get; set; }
        public DocumentDescriptionRequestDto Description { get; set; }
    }
}