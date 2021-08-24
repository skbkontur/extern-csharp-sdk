using System;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class StatCuLetterDescription : DocflowDescription
    {
        public string Cu { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public Guid? RelatedDocflowId { get; set; }
        public string Okpo { get; set; }
    }
}