using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftMetaRequest
    {
        [DataMember]
        [Required]
        public SenderRequest Sender { get; set; }

        [DataMember]
        [Required]
        public RecipientInfoRequest Recipient { get; set; }

        [DataMember]
        [Required]
        public AccountInfoRequest Payer { get; set; }

        [DataMember]
        public RelatedDocumentRequest RelatedDocument { get; set; }

        [DataMember]
        public AdditionalInfoRequest AdditionalInfo { get; set; }
    }
}