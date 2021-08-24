using System;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class PfrLetterDescription : DocflowDescription
    {
        public string RegistrationNumber { get; set; }

        /// <summary>field CU is deprecated and ought to be not used</summary>
        public string Cu { get; set; }

        public string FinalRecipient { get; set; }
        public string Subject { get; set; }
        public string FormType { get; set; }
        public Guid? RelatedDocflowId { get; set; }
    }
}