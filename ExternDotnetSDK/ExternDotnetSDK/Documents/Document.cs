using System;
using ExternDotnetSDK.Common;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ExternDotnetSDK.Documents
{
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