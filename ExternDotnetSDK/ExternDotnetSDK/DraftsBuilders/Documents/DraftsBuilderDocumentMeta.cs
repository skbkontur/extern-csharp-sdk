using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.DraftsBuilders.Documents.Data;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.Documents
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class DraftsBuilderDocumentMeta
    {
        [Required]
        [DataMember]
        public Urn BuilderType { get; set; }

        [Required]
        [DataMember]
        public DraftsBuilderDocumentData BuilderData { get; set; }
    }
}