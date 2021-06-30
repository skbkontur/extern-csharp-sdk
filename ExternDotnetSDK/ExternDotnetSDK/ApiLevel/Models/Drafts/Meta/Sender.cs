using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Sender
    {
        [DataMember]
        [Required]
        public string Inn { get; set; }

        [DataMember]
        public string Kpp { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public Certificate Certificate { get; set; }

        [DataMember]
        [Required]
        public bool IsRepresentative { get; set; }

        [DataMember]
        [Required]
        [JsonProperty("ipaddress")]
        public string IpAddress { get; set; }
    }
}