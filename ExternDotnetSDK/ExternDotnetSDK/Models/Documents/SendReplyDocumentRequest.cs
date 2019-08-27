// ReSharper disable UnusedAutoPropertyAccessor.Global

using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class SendReplyDocumentRequest
    {
        public string SenderIp { get; set; }
    }
}