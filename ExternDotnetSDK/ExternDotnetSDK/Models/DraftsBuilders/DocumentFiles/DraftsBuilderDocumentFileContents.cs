using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.DocumentFiles
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderDocumentFileContents
    {
        public string Base64Content { get; set; }
        public string Base64SignatureContent { get; set; }
        public DraftsBuilderDocumentFileMetaRequest Meta { get; set; }
    }
}