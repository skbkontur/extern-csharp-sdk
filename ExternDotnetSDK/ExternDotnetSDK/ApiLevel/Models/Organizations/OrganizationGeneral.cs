using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Organizations
{
    public class OrganizationGeneral
    {
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Name { get; set; }
        public bool IsMainOrg { get; set; }
        public Link[] Links { get; set; }
    }
}