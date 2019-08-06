using System;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Meta
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