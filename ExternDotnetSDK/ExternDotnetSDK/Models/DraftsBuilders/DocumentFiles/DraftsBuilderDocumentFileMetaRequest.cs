using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.DraftsBuilders.DocumentFiles.Data;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.DraftsBuilders.DocumentFiles
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderDocumentFileMetaRequest
    {
        [Required]
        [DataMember]
        public string FileName { get; set; }

        [Required]
        [DataMember]
        public DraftsBuilderDocumentFileData BuilderData { get; set; }
    }
}