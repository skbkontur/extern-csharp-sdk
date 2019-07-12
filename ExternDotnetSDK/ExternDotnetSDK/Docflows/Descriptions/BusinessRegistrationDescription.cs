using Newtonsoft.Json;

namespace ExternDotnetSDK.Docflows.Descriptions
{
    [JsonObject]
    public class BusinessRegistrationDescription : DocflowDescription
    {
        public string Recipient { get; set; }
        public string SenderInn { get; set; }
    }
}