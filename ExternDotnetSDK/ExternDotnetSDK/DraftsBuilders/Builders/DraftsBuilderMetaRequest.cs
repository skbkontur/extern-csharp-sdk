using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.Drafts.Requests;
using ExternDotnetSDK.DraftsBuilders.Builders.Data;

namespace ExternDotnetSDK.DraftsBuilders.Builders
{
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