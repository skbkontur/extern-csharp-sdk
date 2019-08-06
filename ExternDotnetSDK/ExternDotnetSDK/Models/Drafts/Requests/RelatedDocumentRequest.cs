using System;
using System.Runtime.Serialization;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Requests
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RelatedDocumentRequest
    {
        /// <summary>Идентификатор связанного ДО</summary>
        [DataMember]
        public Guid RelatedDocflowId { get; set; }

        /// <summary>Идентификатор документа в ДО</summary>
        [DataMember]
        public Guid RelatedDocumentId { get; set; }
    }
}