using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.Drafts.Requests;
using KeApiOpenSdk.Models.DraftsBuilders.Builders.Data;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.DraftsBuilders.Builders
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