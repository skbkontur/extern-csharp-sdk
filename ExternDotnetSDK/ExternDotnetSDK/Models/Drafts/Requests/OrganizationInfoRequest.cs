using System.Runtime.Serialization;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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