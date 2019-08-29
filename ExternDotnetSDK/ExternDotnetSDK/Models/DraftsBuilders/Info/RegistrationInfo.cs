using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.DraftsBuilders.Info
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RegistrationInfo
    {
        [Required]
        [DataMember]
        public ApplicantInfo[] ApplicantInfos { get; set; }

        // [Required] // сделать Required, когда у клиентов обновится утилита
        [DataMember]
        public Urn BusinessType { get; set; }

        [DataMember]
        public IpInfo IpInfo { get; set; }

        [DataMember]
        public UlInfo UlInfo { get; set; }
    }
}