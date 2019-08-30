using System.Runtime.Serialization;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Meta
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RecipientInfo
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