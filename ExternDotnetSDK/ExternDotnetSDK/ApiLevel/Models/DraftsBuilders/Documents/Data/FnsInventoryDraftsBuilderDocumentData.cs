using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data
{
    public class FnsInventoryDraftsBuilderDocumentData : DraftsBuilderDocumentData
    {
        // [JsonProperty(Required = Required.Always)]
        public string ClaimItemNumber { get; set; }

        // [JsonProperty(Required = Required.Always)]
        public string ScannedDocumentDate { get; set; }

        // [JsonProperty(Required = Required.Always)]
        public string ScannedDocumentNumber { get; set; }

        public DraftsBuilderDocumentType? Type { get; set; }
    }
}