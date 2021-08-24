using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    public class OrganizationInfo
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