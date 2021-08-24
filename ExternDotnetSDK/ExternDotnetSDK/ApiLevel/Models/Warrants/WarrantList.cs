using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    public class WarrantList
    {
        public long Skip { get; set; }
        public long Take { get; set; }
        public long TotalCount { get; set; }
        public Warrant[] Warrants { get; set; }
        public Link[] Links { get; set; }
    }
}