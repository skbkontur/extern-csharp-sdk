using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts.Meta;
using ExternDotnetSDK.Models.DraftsBuilders.Builders.Data;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.DraftsBuilders.Builders
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