// ReSharper disable UnusedAutoPropertyAccessor.Global

using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class SendReplyDocumentRequest
    {
        public string SenderIp { get; set; }
    }
}