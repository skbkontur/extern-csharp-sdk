using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Organizations
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class OrganizationGeneral
    {
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Name { get; set; }
        public bool IsMainOrg { get; set; }
        public Link[] Links { get; set; }
    }
}