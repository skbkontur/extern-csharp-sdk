using System;
using System.Runtime.Serialization;

namespace ExternDotnetSDK.Drafts.Requests
{
    [DataContract]
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