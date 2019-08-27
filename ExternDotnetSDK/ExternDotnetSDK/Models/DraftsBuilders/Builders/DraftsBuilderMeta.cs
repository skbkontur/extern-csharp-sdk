using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.Drafts.Meta;
using KeApiOpenSdk.Models.DraftsBuilders.Builders.Data;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderMeta
    {
        [Required]
        [DataMember]
        public Sender Sender { get; set; }

        [Required]
        [DataMember]
        public AccountInfo Payer { get; set; }

        [Required]
        [DataMember]
        public RecipientInfo Recipient { get; set; }

        [Required]
        [DataMember]
        public Urn BuilderType { get; set; }

        [Required]
        [DataMember]
        public DraftsBuilderData BuilderData { get; set; }
    }
}