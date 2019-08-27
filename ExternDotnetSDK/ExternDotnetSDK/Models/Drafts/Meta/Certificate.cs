using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Drafts.Meta
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Certificate
    {
        [DataMember]
        [Required]
        public string Content { get; set; }
    }
}