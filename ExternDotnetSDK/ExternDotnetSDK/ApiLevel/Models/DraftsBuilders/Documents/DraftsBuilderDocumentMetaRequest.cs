using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents
{
    public class DraftsBuilderDocumentMetaRequest
    {
        // [JsonProperty(Required = Required.Always)]
        public DraftsBuilderDocumentData BuilderData { get; set; }
    }
}