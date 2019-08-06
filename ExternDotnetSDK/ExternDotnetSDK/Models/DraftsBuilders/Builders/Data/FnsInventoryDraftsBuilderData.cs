using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.Drafts.Requests;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.DraftsBuilders.Builders.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class FnsInventoryDraftsBuilderData : DraftsBuilderData
    {
        [Required]
        [DataMember]
        public string ClaimItemNumber { get; set; }

        [Required]
        [DataMember]
        public RelatedDocumentRequest RelatedDocument { get; set; }
    }
}