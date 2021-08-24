using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles
{
    public class DraftsBuilderFileMetaRequest
    {
        /// <summary>
        /// Название файла
        /// </summary>
        [Required]
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// Сведения о файле
        /// </summary>
        [Required]
        [DataMember]
        public DraftsBuilderFileData BuilderData { get; set; }
    }
}