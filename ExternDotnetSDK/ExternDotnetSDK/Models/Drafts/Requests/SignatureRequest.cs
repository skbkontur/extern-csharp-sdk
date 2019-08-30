using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Requests
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class SignatureRequest
    {
        public string Base64Content { get; set; }
    }
}