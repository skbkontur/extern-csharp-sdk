using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles.Data
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