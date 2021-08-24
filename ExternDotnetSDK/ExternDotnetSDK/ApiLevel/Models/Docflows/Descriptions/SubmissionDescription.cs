using System;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class SubmissionDescription : DocflowDescription
    {
        public FormVersion FormVersion { get; set; }
        public string Recipient { get; set; }
        public string FinalRecipient { get; set; }
        public string Ogrn { get; set; }
        public DateTime PeriodBegin { get; set; }
        public DateTime PeriodEnd { get; set; }
        public Guid? RelatedDocflowId { get; set; }
        public Guid? RelatedDocumentId { get; set; }
    }
}