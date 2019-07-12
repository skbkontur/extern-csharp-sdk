using ExternDotnetSDK.Docflows.Common;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Docflows.Descriptions
{
    [JsonObject]
    public class ApplicationDescription : DocflowDescription
    {
        public FormVersion FormVersion { get; set; }
        public string Recipient { get; set; }
        public string FinalRecipient { get; set; }
        public string DocumentNumber { get; set; }
    }
}