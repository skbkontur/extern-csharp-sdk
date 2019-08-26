using System;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Meta
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RelatedDocument
    {
        public Guid RelatedDocflowId { get; set; }

        public Guid RelatedDocumentId { get; set; }
    }
}