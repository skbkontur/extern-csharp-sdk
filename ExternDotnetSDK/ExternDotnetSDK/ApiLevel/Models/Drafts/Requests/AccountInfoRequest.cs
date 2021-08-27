namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    public class AccountInfoRequest
    {
        private OrganizationInfoRequest organization;
        
        //[JsonProperty(Required = Required.Always)]
        public string Inn { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public OrganizationInfoRequest Organization
        {
            get => organization;
            set => organization = value ?? new OrganizationInfoRequest();
        }

        public string RegistrationNumberPfr { get; set; }
        public string RegistrationNumberFss { get; set; }
    }
}