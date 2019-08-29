using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.DraftsBuilders.Info
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