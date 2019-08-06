using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class SenderRequest
    {
        /// <summary>ИНН</summary>
        [DataMember]
        [Required]
        public string Inn { get; set; }

        /// <summary>КПП</summary>
        [DataMember]
        public string Kpp { get; set; }

        /// <summary>Сертификат для отправки</summary>
        [DataMember]
        [Required]
        public CertificateRequest Certificate { get; set; }

        /// <summary>Отправитель является представителем</summary>
        [DataMember]
        [Required]
        public bool IsRepresentative { get; set; }

        /// <summary>IP адрес отправителя отчета</summary>
        [DataMember]
        [Required]
        [JsonProperty("ipaddress")]
        public string IpAddress { get; set; }
    }
}