using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public abstract class DocflowDescription
    {
    }
}