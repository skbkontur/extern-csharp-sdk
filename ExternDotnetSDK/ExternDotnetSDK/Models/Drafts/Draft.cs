using System;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.Drafts.Meta;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Draft
    {
        public Guid Id { get; set; }
        public Link[] Docflows { get; set; }
        public Link[] Documents { get; set; }
        public DraftMeta Meta { get; set; }
        public DraftStatus Status { get; set; }
        public Link[] Links { get; set; }
    }
}