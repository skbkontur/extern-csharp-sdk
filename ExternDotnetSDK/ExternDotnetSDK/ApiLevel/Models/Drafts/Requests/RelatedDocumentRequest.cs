using System;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    public class RelatedDocumentRequest
    {
        [DataMember]
        public Guid RelatedDocflowId { get; set; }

        [DataMember]
        public Guid RelatedDocumentId { get; set; }
    }
}