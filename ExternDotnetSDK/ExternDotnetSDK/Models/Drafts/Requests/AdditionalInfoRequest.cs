using System.Runtime.Serialization;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Drafts.Requests
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