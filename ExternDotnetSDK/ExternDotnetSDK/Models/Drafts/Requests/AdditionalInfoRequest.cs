using System.Runtime.Serialization;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Drafts.Requests
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