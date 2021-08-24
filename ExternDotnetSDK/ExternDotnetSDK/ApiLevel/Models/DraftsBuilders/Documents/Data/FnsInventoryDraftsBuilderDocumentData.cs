using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data
{
    public class FnsInventoryDraftsBuilderDocumentData : DraftsBuilderDocumentData
    {
        [Required]
        [DataMember]
        public string ClaimItemNumber { get; set; }

        [Required]
        [DataMember]
        public string ScannedDocumentDate { get; set; }

        [Required]
        [DataMember]
        public string ScannedDocumentNumber { get; set; }

        [DataMember]
        public DraftsBuilderDocumentType? Type { get; set; }
    }
}