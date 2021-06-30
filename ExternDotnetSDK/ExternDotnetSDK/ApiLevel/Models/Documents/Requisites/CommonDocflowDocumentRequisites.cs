using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents.Requisites
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CommonDocflowDocumentRequisites : DocflowDocumentRequisites
    {
    }
}