using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExternDotnetSDK.Drafts.Requests
{
    /// <summary>Учетная запись организации</summary>
    [DataContract]
    public class AccountInfoRequest
    {
        private OrganizationInfoRequest organization;
        /// <summary>ИНН</summary>
        [DataMember]
        [Required]
        public string Inn { get; set; }

        /// <summary>ЮЛ специфичные</summary>
        [DataMember]
        public OrganizationInfoRequest Organization
        {
            get => organization;
            set => organization = value ?? new OrganizationInfoRequest();
        }

        /// <summary>Регистрационный номер ПФР</summary>
        [DataMember]
        public string RegistrationNumberPfr { get; set; }

        /// <summary>Регистрационный номер ФСС</summary>
        [DataMember]
        public string RegistrationNumberFss { get; set; }
    }
}