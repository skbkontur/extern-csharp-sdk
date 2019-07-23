using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.Info
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
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