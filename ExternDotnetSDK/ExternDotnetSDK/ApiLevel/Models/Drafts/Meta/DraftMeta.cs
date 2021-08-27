namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    public class DraftMeta
    {
        //[JsonProperty(Required = Required.Always)]
        public Sender Sender { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public RecipientInfo Recipient { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public AccountInfo Payer { get; set; }

        public RelatedDocument RelatedDocument { get; set; }

        public AdditionalInfo AdditionalInfo { get; set; }
    }
}