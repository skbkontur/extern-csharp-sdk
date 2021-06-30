using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class SignResult
    {
        public Link[] SignedDocuments { get; set; }
    }
}