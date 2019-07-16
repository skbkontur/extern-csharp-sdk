using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExternDotnetSDK.Drafts.Meta
{
    [DataContract]
    public class Sender
    {
        /// <summary>ИНН</summary>
        [DataMember]
        [Required]
        public string Inn { get; set; }

        /// <summary>КПП</summary>
        [DataMember]
        public string Kpp { get; set; }

        /// <summary>Название</summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>Сертификат для отправки</summary>
        [DataMember]
        [Required]
        public Certificate Certificate { get; set; }

        /// <summary>Отправитель является представителем</summary>
        [DataMember]
        [Required]
        public bool IsRepresentative { get; set; }

        /// <summary>IP адрес отправителя отчета</summary>
        [DataMember]
        [Required]
        public string IpAddress { get; set; }
    }
}