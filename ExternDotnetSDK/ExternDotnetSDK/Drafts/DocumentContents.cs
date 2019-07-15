using System;
using System.Collections.Generic;
using System.Text;
using ExternDotnetSDK.Drafts.Requests;

namespace ExternDotnetSDK.Drafts
{
    public class DocumentContents
    {
        public string Base64Content { get; set; }
        public string Signature { get; set; }
        public DocumentDescriptionRequestDto Description { get; set; }
    }
}
