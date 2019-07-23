
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Docflows
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class DocflowPage
    {
        public long Skip { get; set; }
        public long Take { get; set; }
        public long TotalCount { get; set; }
        public DocflowPageItem[] DocflowsPageItem { get; set; }
    }
}