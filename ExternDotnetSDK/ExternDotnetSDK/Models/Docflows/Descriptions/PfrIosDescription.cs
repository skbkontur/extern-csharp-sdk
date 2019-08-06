using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PfrIosDescription : DocflowDescription
    {
        public string RegistrationNumber { get; set; }

        /// <summary>field CU is deprecated and ought to be not used</summary>
        public string Cu { get; set; }

        public string FinalRecipient { get; set; }
        public string FormType { get; set; }
        public FormVersion FormVersion { get; set; }
    }
}