using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PfrReportDescription : DocflowDescription
    {
        /// <summary>field CU is deprecated and ought to be not used</summary>
        public string Cu { get; set; }

        public string RegistrationNumber { get; set; }
        public string FinalRecipient { get; set; }
        public FormVersion FormVersion { get; set; }
        public DateTime? PeriodBegin { get; set; }
        public DateTime? PeriodEnd { get; set; }
    }
}