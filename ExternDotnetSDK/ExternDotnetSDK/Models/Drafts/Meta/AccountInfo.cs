using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Meta
{
    /// <summary>Учетная запись организации</summary>
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class AccountInfo
    {
        private OrganizationInfo organization;

        /// <summary>ИНН</summary>
        [DataMember]
        [Required]
        public string Inn { get; set; }

        /// <summary>Название</summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>ЮЛ специфичные</summary>
        [DataMember]
        public OrganizationInfo Organization
        {
            get => organization;
            set => organization = value ?? new OrganizationInfo();
        }

        /// <summary>Регистрационный номер ФСС</summary>
        [DataMember]
        public string RegistrationNumberFss { get; set; }

        /// <summary>Регистрационный номер ПФР</summary>
        [DataMember]
        public string RegistrationNumberPfr { get; set; }
    }
}