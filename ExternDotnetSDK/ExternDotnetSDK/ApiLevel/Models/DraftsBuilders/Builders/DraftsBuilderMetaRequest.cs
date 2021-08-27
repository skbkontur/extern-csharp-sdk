using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders
{
    public class DraftsBuilderMetaRequest
    {
        /// <summary>
        /// Информация об отправителе
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public SenderRequest Sender { get; set; }

        /// <summary>
        /// Информация о налогоплательщике
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public AccountInfoRequest Payer { get; set; }

        /// <summary>
        /// Информация о получателе, контролирующий орган
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public RecipientInfoRequest Recipient { get; set; }

        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public Urn BuilderType { get; set; }

        /// <summary>
        /// Данные для указанного типа DraftsBuilder
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public DraftsBuilderData BuilderData { get; set; }
    }
}