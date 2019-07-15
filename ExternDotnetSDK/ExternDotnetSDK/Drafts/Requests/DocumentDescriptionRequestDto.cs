using ExternDotnetSDK.Common;

namespace ExternDotnetSDK.Drafts.Requests
{
    public class DocumentDescriptionRequestDto
    {
        public Urn Type { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
    }
}