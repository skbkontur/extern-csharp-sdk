using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts.Meta
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class DraftMeta
    {
        /// <summary>Отправитель</summary>
        [DataMember]
        [Required]
        public Sender Sender { get; set; }

        /// <summary>Получатель</summary>
        [DataMember]
        [Required]
        public RecipientInfo Recipient { get; set; }

        /// <summary>Организация, за которую отправляется отчет</summary>
        [DataMember]
        [Required]
        public AccountInfo Payer { get; set; }

        /// <summary>Связанный ДО</summary>
        [DataMember]
        public RelatedDocument RelatedDocument { get; set; }

        /// <summary>Доп.инфа в мете, например, тема письма</summary>
        [DataMember]
        public AdditionalInfo AdditionalInfo { get; set; }
    }
}