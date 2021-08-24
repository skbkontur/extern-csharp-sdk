namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class IonDescription : DocflowDescription
    {
        public FormVersion FormVersion { get; set; }
        public string Recipient { get; set; }
        public string FinalRecipient { get; set; }
        public string ServiceCode { get; set; }
    }
}