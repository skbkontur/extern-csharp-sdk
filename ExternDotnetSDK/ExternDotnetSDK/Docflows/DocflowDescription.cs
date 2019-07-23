using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Docflows
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public abstract class DocflowDescription
    {
    }
}