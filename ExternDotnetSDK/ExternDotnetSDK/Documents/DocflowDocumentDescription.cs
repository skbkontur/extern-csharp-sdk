using ExternDotnetSDK.Common;
using ExternDotnetSDK.Documents.Requisites;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Documents
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
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