namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    public class DraftMetaRequest
    {
        //[JsonProperty(Required = Required.Always)]
        public SenderRequest Sender { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public RecipientInfoRequest Recipient { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public AccountInfoRequest Payer { get; set; }
        public RelatedDocumentRequest RelatedDocument { get; set; }
        public AdditionalInfoRequest AdditionalInfo { get; set; }
    }
}