// ReSharper disable UnusedAutoPropertyAccessor.Global

using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Documents.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PrintDocumentData
    {
        public string Content { get; set; }
    }
}