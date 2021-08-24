using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders
{
    public class DraftsBuilderMetaRequest
    {
        /// <summary>
        /// Информация об отправителе
        /// </summary>
        [Required]
        [DataMember]
        public SenderRequest Sender { get; set; }

        /// <summary>
        /// Информация о налогоплательщике
        /// </summary>
        [Required]
        [DataMember]
        public AccountInfoRequest Payer { get; set; }

        /// <summary>
        /// Информация о получателе, контролирующий орган
        /// </summary>
        [Required]
        [DataMember]
        public RecipientInfoRequest Recipient { get; set; }

        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        [Required]
        [DataMember]
        public Urn BuilderType { get; set; }

        /// <summary>
        /// Данные для указанного типа DraftsBuilder
        /// </summary>
        [Required]
        [DataMember]
        public DraftsBuilderData BuilderData { get; set; }
    }
}