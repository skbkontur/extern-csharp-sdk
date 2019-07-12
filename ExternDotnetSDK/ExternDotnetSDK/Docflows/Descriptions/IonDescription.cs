using ExternDotnetSDK.Docflows.Common;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Docflows.Descriptions
{
    [JsonObject]
    public class IonDescription : DocflowDescription
    {
        public FormVersion FormVersion { get; set; }
        public string Recipient { get; set; }
        public string FinalRecipient { get; set; }
        public string ServiceCode { get; set; }
    }
}