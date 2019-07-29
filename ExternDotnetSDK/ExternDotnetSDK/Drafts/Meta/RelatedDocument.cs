using System;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts.Meta
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RelatedDocument
    {
        /// <summary>Идентификатор связанного ДО</summary>
        public Guid RelatedDocflowId { get; set; }

        /// <summary>Идентификатор документа в ДО</summary>
        public Guid RelatedDocumentId { get; set; }
    }
}