using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using KeApiOpenSdk.Models.Drafts.Requests;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.DraftsBuilders.Builders.Data
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