using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles.Data;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles
{
    public class DraftsBuilderDocumentFileMeta
    {
        // [JsonProperty(Required = Required.Always)]
        public string FileName { get; set; }

        // [JsonProperty(Required = Required.Always)]
        public Urn BuilderType { get; set; }

        // [JsonProperty(Required = Required.Always)]
        public DraftsBuilderFileData BuilderData { get; set; }
    }
}