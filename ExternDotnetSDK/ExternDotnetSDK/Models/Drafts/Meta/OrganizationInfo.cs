using System.Runtime.Serialization;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Meta
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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