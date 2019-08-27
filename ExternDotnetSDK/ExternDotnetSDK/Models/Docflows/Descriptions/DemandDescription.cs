using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DemandDescription : DocflowDescription
    {
        public int AttachmentsCount { get; set; }
        public FormVersion[] FormVersions { get; set; }
        public string Cu { get; set; }
        public string TransitCu { get; set; }
    }
}