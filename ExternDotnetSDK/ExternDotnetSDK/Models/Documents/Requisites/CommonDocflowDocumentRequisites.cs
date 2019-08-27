using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Documents.Requisites
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CommonDocflowDocumentRequisites : DocflowDocumentRequisites
    {
    }
}