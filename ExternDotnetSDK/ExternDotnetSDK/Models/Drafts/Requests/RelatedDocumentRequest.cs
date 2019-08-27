using System;
using System.Runtime.Serialization;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RelatedDocumentRequest
    {
        [DataMember]
        public Guid RelatedDocflowId { get; set; }

        [DataMember]
        public Guid RelatedDocumentId { get; set; }
    }
}