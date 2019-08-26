using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CertificateRequest
    {
        [DataMember]
        [Required]
        public string Content { get; set; }
    }
}