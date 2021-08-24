using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Documents.Requisites;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents
{
    public class DocflowDocumentDescription
    {
        public Urn Type { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public bool Compressed { get; set; }
        public DocflowDocumentRequisites Requisites { get; set; }
        public long? RelatedDocflowsCount { get; set; }
        public bool SupportRecognition { get; set; }
    }
}