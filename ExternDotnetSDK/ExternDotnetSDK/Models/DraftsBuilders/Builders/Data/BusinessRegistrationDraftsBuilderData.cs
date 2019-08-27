using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiOpenSdk.Models.DraftsBuilders.Info;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.DraftsBuilders.Builders.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class BusinessRegistrationDraftsBuilderData : DraftsBuilderData
    {
        [Required]
        [DataMember]
        public RegistrationInfo RegistrationInfo { get; set; }
    }
}