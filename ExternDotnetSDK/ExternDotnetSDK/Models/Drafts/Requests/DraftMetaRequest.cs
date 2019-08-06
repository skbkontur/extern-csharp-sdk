using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftMetaRequest
    {
        /// <summary>Отправитель</summary>
        [DataMember]
        [Required]
        public SenderRequest Sender { get; set; }

        /// <summary>Получатель</summary>
        [DataMember]
        [Required]
        public RecipientInfoRequest Recipient { get; set; }

        /// <summary>Организация, за которую отправляется отчет</summary>
        [DataMember]
        [Required]
        public AccountInfoRequest Payer { get; set; }

        /// <summary>Связанный ДО</summary>
        [DataMember]
        public RelatedDocumentRequest RelatedDocument { get; set; }

        /// <summary>Доп.инфа в мете, например, тема письма</summary>
        [DataMember]
        public AdditionalInfoRequest AdditionalInfo { get; set; }
    }
}