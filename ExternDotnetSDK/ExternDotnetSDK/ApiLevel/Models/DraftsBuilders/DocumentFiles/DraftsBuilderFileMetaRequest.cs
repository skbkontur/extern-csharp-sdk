using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles.Data;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles
{
    public class DraftsBuilderFileMetaRequest
    {
        /// <summary>
        /// Название файла
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public string FileName { get; set; }

        /// <summary>
        /// Сведения о файле
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public DraftsBuilderFileData BuilderData { get; set; }
    }
}