using System;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.Drafts.Meta;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Drafts
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