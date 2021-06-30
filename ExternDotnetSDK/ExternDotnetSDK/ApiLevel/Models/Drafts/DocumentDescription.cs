using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocumentDescription
    {
        public Urn Type { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}