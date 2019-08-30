// ReSharper disable UnusedAutoPropertyAccessor.Global

using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Documents.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PrintDocumentData
    {
        public string Content { get; set; }
    }
}