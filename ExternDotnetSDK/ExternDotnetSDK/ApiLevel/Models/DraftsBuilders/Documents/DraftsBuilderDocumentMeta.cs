using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents
{
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