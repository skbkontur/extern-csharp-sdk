using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Documents;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts
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