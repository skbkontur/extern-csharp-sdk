using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    public class RelatedDocument
    {
        public Guid RelatedDocflowId { get; set; }

        public Guid RelatedDocumentId { get; set; }
    }
}