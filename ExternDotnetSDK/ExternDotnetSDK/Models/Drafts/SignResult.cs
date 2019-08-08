using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class SignResult
    {
        public Link[] SignedDocuments { get; set; }
    }
}