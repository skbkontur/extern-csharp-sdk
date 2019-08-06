using System;
using System.Collections.Generic;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftDocument
    {
        public Guid Id { get; set; }
        public Link DecryptedContentLink { get; set; }
        public Link EncryptedContentLink { get; set; }
        public Link SignatureContentLink { get; set; }
        public List<Signature> Signatures { get; set; }
        public DocumentDescription Description { get; set; }
    }
}