using System.Collections.Generic;
using ExternDotnetSDK.Common;

namespace ExternDotnetSDK.Drafts
{
    public class DocumentDescription
    {
        public Urn Type { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}