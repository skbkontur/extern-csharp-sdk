using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Errors
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CheckError
    {
        public string Description { get; set; }
        public string Source { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }
        public string Tags { get; set; }
        public string Id { get; set; }
    }
}