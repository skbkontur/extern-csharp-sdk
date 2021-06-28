using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.DocumentFiles.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class FnsInventoryDraftsBuilderFileData : DraftsBuilderFileData
    {
        /// <summary>
        /// Порядковый номер файла в многостраничном документе
        /// </summary>
        public int ScannedFileOrder { get; set; }
    }
}