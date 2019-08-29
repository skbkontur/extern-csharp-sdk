using System.Runtime.Serialization;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RecipientInfoRequest
    {
        [DataMember]
        public string IfnsCode { get; set; }

        [DataMember]
        public string MriCode { get; set; }

        [DataMember]
        public string TogsCode { get; set; }

        [DataMember]
        public string UpfrCode { get; set; }

        [DataMember]
        public string FssCode { get; set; }
    }
}