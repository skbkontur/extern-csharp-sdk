using System;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Docflows.Descriptions
{
    [JsonObject]
    public class CuLetterDescription : DocflowDescription
    {
        public string Cu { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public Guid? RelatedDocflowId { get; set; }
        public Guid? RelatedDocumentId { get; set; }
    }
}