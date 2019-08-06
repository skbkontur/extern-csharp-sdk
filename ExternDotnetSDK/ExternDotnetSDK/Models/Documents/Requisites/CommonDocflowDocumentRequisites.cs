using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Documents.Requisites
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CommonDocflowDocumentRequisites : DocflowDocumentRequisites
    {
    }
}