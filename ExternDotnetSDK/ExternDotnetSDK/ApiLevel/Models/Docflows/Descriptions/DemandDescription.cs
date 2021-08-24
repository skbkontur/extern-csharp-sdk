namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class DemandDescription : DocflowDescription
    {
        public int AttachmentsCount { get; set; }
        public FormVersion[] FormVersions { get; set; }
        public string Cu { get; set; }
        public string TransitCu { get; set; }
    }
}