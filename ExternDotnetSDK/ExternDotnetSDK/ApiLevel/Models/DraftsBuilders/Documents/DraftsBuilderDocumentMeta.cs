using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents
{
    public class DraftsBuilderDocumentMeta
    {
        // [JsonProperty(Required = Required.Always)]
        public Urn BuilderType { get; set; }

        // [JsonProperty(Required = Required.Always)]
        public DraftsBuilderDocumentData BuilderData { get; set; }
    }
}