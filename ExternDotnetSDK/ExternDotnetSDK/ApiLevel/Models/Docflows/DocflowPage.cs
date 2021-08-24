namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    public class DocflowPage
    {
        public long Skip { get; set; }
        public long Take { get; set; }
        public long TotalCount { get; set; }
        public DocflowPageItem[] DocflowsPageItem { get; set; }
    }
}