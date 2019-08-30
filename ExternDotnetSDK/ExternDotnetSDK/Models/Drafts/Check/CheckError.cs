using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Check
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