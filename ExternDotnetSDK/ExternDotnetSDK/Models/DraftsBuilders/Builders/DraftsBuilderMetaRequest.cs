using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Drafts.Requests;
using Kontur.Extern.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderMetaRequest
    {
        [Required]
        [DataMember]
        public SenderRequest Sender { get; set; }

        [Required]
        [DataMember]
        public AccountInfoRequest Payer { get; set; }

        [Required]
        [DataMember]
        public RecipientInfoRequest Recipient { get; set; }

        [Required]
        [DataMember]
        public Urn BuilderType { get; set; }

        [Required]
        [DataMember]
        public DraftsBuilderData BuilderData { get; set; }
    }
}