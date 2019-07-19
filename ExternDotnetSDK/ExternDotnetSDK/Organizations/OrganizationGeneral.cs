using ExternDotnetSDK.Common;

namespace ExternDotnetSDK.Organizations
{
    //WHERE IS NAMING STRATEGY AND JSON CONVERTER FOR ALL THIS NAMESPACE?!
    public class OrganizationGeneral
    {
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Name { get; set; }
        public bool IsMainOrg { get; set; }
        public Link[] Links { get; set; }
    }
}