using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Documents.Requisites
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class CommonDocflowDocumentRequisites : DocflowDocumentRequisites
    {
    }
}