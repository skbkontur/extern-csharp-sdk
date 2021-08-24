using System;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class PfrCuLetterDescription : DocflowDescription
    {
        public string Cu { get; set; }
        public string RegistrationNumber { get; set; }
        public string Subject { get; set; }
        public string FormType { get; set; }
        public Guid? RelatedDocflowId { get; set; }
    }
}