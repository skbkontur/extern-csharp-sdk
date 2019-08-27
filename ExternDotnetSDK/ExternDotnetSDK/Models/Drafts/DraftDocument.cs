using System;
using System.Collections.Generic;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Drafts
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