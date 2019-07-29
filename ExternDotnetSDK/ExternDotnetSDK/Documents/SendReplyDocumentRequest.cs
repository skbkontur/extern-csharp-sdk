// ReSharper disable UnusedAutoPropertyAccessor.Global

using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class SendReplyDocumentRequest
    {
        public string SenderIp { get; set; }
    }
}