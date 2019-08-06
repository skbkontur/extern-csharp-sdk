// ReSharper disable UnusedAutoPropertyAccessor.Global

using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Documents.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PrintDocumentData
    {
        public string Content { get; set; }
    }
}