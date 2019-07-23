using System.Collections.Generic;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class DocumentDescription
    {
        public Urn Type { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}