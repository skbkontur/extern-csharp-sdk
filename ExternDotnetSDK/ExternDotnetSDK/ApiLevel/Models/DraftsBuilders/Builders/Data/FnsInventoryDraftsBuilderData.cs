using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data
{
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