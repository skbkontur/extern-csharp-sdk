using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
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