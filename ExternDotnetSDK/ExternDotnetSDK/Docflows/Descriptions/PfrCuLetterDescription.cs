using System;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class PfrCuLetterDescription : DocflowDescription
    {
        public string Cu { get; set; }
        public string RegistrationNumber { get; set; }
        public string Subject { get; set; }
        public string FormType { get; set; }
        public Guid? RelatedDocflowId { get; set; }
    }
}