using System.Runtime.Serialization;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class AdditionalInfoRequest
    {
        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string[] AdditionalCertificates { get; set; }
    }
}