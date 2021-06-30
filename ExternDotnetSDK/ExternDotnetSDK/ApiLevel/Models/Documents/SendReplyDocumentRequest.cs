// ReSharper disable UnusedAutoPropertyAccessor.Global

using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class SendReplyDocumentRequest
    {
        public string SenderIp { get; set; }
    }
}