using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    public class OrganizationInfoRequest
    {
        private string kpp;

        [DataMember]
        public string Kpp
        {
            get => kpp;
            set => kpp = value == "" ? null : value;
        }
    }
}