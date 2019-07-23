using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.DraftsBuilders.DocumentFiles.Data;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.DocumentFiles
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
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
        public DraftsBuilderDocumentFileData BuilderData { get; set; }
    }
}