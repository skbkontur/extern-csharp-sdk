using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles
{
    public class DraftsBuilderDocumentFileMeta
    {
        [Required]
        [DataMember]
        public string FileName { get; set; }

        [Required]
        [DataMember]
        public Urn BuilderType { get; set; }

        [Required]
        [DataMember]
        public DraftsBuilderFileData BuilderData { get; set; }
    }
}