using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class SignatureRequest
    {
        public string Base64Content { get; set; }
    }
}