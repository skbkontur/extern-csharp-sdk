using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Common;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Kontur.Extern.Client.ApiLevel.Models.Documents
{
    public class Document
    {
        public Guid Id { get; set; }
        public DocflowDocumentDescription Description { get; set; }
        public Content Content { get; set; }
        public DateTime? SendDate { get; set; }
        public Signature[] Signatures { get; set; }
        public List<Link> Links { get; set; }
    }
}