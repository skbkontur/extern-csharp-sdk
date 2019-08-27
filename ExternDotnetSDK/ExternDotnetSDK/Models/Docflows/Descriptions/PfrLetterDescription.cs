using System;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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