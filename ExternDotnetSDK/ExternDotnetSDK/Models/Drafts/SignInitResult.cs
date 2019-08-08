using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Documents;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class SignInitResult
    {
        public Link[] Links { get; set; }
        public Link[] DocumentsToSign { get; set; }
        public string RequestId { get; set; }
        public string TaskId { get; set; }
        public ConfirmTypeInternal ConfirmType { get; set; }
    }
}