using System.Runtime.Serialization;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts.Requests
{
    /// <summary>Реквизиты, специфичные для ЮЛ</summary>
    [DataContract]
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class OrganizationInfoRequest
    {
        private string kpp;

        /// <summary>КПП</summary>
        [DataMember]
        public string Kpp
        {
            get => kpp;
            set => kpp = value == "" ? null : value;
        }
    }
}