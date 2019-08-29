using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.Drafts.Requests;
using KeApiClientOpenSdk.Models.DraftsBuilders.Builders.Data;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.DraftsBuilders.Builders
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