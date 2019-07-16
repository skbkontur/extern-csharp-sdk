using Newtonsoft.Json;

namespace ExternDotnetSDK.Docflows.Descriptions
{
    [JsonObject]
    public class DemandDescription : DocflowDescription
    {
        public int AttachmentsCount { get; set; }
        public FormVersion[] FormVersions { get; set; }
        public string Cu { get; set; }
        public string TransitCu { get; set; }
    }
}