using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Organizations
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class OrganizationBatch
    {
        public Organization[] Organizations { get; set; }
        public long TotalCount { get; set; }
        public Link[] Links { get; set; }
        public long Skip { get; set; }
        public long Take { get; set; }
    }
}