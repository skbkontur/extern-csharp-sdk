// ReSharper disable UnusedAutoPropertyAccessor.Global

using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Documents.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PrintDocumentData
    {
        public string Content { get; set; }
    }
}