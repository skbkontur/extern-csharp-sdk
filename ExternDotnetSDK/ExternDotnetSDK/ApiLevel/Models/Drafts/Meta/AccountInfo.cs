using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    public class AccountInfo
    {
        private OrganizationInfo organization;

        //[JsonProperty(Required = Required.Always)]
        public string Inn { get; set; }

        public string Name { get; set; }

        public OrganizationInfo Organization
        {
            get => organization;
            set => organization = value ?? new OrganizationInfo();
        }

        public string RegistrationNumberFss { get; set; }

        public string RegistrationNumberPfr { get; set; }
    }
}