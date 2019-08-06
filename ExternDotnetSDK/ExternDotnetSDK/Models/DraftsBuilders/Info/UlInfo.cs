using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.DraftsBuilders.Info
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class UlInfo
    {
        [Required]
        [DataMember]
        public string Ogrn { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }
    }
}