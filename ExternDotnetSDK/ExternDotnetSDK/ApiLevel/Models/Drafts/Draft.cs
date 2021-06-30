using System;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts
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