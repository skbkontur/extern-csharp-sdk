using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents
{
    [PublicAPI]
    public class Content
    {
        public List<DocflowDocumentContents>? DocflowDocumentContents { get; set; }
    }

    [PublicAPI]
    public class DocflowDocumentContents
    {
        public Guid ContentId { get; set; }
        public bool Encrypted { get; set; }
        public bool Compressed { get; set; }
    }
}