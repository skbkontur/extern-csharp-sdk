using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public abstract class DraftsBuilderDocumentData
    {
    }
}