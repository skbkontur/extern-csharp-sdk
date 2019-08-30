using System;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Kontur.Extern.Client.Models.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Document
    {
        public Guid Id { get; set; }
        public DocflowDocumentDescription Description { get; set; }
        public Content Content { get; set; }
        public DateTime? SendDate { get; set; }
        public Signature[] Signatures { get; set; }
        public Link[] Links { get; set; }
    }
}