using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Organizations
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class OrganizationGeneral
    {
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Name { get; set; }
        public bool IsMainOrg { get; set; }
        public Link[] Links { get; set; }
    }
}