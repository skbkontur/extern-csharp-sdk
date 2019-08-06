using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts.Requests;
using ExternDotnetSDK.Models.DraftsBuilders.Builders.Data;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.DraftsBuilders.Builders
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