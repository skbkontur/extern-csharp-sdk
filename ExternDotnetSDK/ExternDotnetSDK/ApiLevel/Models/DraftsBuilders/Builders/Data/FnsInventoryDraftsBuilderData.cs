using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data
{
    public class FnsInventoryDraftsBuilderData : DraftsBuilderData
    {
        [JsonProperty(Required = Required.Always)]
        public string ClaimItemNumber { get; set; }

        [JsonProperty(Required = Required.Always)]
        public RelatedDocumentRequest RelatedDocument { get; set; }
    }
}