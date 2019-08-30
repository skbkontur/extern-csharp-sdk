using System;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Meta
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RelatedDocument
    {
        public Guid RelatedDocflowId { get; set; }

        public Guid RelatedDocumentId { get; set; }
    }
}