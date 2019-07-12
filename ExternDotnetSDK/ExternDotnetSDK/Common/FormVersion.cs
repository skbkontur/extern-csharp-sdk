using Newtonsoft.Json;

namespace ExternDotnetSDK.Docflows.Common
{
    [JsonObject]
    public class FormVersion
    {
        public string Knd { get; set; }
        public string Okud { get; set; }
        public string Version { get; set; }
        public string FormFullname { get; set; }
        public string FormShortname { get; set; }
    }
}