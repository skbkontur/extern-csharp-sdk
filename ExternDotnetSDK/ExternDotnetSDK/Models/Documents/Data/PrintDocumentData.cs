// ReSharper disable UnusedAutoPropertyAccessor.Global

using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Documents.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PrintDocumentData
    {
        public string Content { get; set; }
    }
}