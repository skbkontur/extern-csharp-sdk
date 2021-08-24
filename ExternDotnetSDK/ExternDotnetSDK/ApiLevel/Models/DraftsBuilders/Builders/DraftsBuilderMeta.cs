using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders
{
    public class DraftsBuilderMeta
    {
        [JsonProperty(Required = Required.Always)]
        public Sender Sender { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public AccountInfo Payer { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public RecipientInfo Recipient { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public Urn BuilderType { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public DraftsBuilderData BuilderData { get; set; }
    }
}