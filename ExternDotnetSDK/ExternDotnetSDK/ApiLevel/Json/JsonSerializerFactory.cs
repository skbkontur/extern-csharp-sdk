using Kontur.Extern.Client.ApiLevel.Json.Converters;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingStrategies;

namespace Kontur.Extern.Client.ApiLevel.Json
{
    public static class JsonSerializerFactory
    {
       public static IJsonSerializer CreateJsonSerializer(bool ignoreIndentation = false, bool ignoreNullValues = true) => 
           new SystemTextJsonSerializer(
            new KebabCaseNamingPolicy(),
            new System.Text.Json.Serialization.JsonConverter[]
            {
                new UrnJsonConverter(),
                new DocflowContainingConverter(),
                new DocflowDocumentDescriptionConverter()
            },
            ignoreIndentation,
            ignoreNullValues
        );
    }
}