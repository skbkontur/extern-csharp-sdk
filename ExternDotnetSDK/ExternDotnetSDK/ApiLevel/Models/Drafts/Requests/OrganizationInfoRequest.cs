namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    public class OrganizationInfoRequest
    {
        private string kpp;

        public string Kpp
        {
            get => kpp;
            set => kpp = value == "" ? null : value;
        }
    }
}