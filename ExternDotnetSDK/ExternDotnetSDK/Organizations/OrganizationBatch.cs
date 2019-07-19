using ExternDotnetSDK.Common;

namespace ExternDotnetSDK.Organizations
{
    public class OrganizationBatch
    {
        public Organization[] Organizations { get; set; }
        public long TotalCount { get; set; }
        public Link[] Links { get; set; }
        public long Skip { get; set; }
        public long Take { get; set; }
    }
}