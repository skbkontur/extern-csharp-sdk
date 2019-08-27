using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class AccountInfoRequest
    {
        private OrganizationInfoRequest organization;
        [DataMember]
        [Required]
        public string Inn { get; set; }

        [DataMember]
        public OrganizationInfoRequest Organization
        {
            get => organization;
            set => organization = value ?? new OrganizationInfoRequest();
        }

        [DataMember]
        public string RegistrationNumberPfr { get; set; }

        [DataMember]
        public string RegistrationNumberFss { get; set; }
    }
}