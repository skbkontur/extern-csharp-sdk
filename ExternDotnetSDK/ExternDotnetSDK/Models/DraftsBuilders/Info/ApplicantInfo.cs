using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Info
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