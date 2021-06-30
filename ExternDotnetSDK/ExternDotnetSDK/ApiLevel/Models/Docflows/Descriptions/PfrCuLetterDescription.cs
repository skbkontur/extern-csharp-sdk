using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PfrCuLetterDescription : DocflowDescription
    {
        public string Cu { get; set; }
        public string RegistrationNumber { get; set; }
        public string Subject { get; set; }
        public string FormType { get; set; }
        public Guid? RelatedDocflowId { get; set; }
    }
}