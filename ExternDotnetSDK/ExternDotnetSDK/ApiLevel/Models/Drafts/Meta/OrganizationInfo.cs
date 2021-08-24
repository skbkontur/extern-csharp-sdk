namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    public class OrganizationInfo
    {
        private string kpp;

        public string Kpp
        {
            get => kpp;
            set => kpp = string.IsNullOrEmpty(value) ? null : value;
        }
    }
}