using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Organizations
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