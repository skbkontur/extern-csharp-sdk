using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.DraftsBuilders.Info
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class ApplicantInfo
    {
        [Required]
        [DataMember]
        public string Inn { get; set; }

        [Required]
        [DataMember]
        public Fio Fio { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}