using Kontur.Extern.Client.ApiLevel.Json.Converters;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingStrategies;

namespace Kontur.Extern.Client.ApiLevel.Json
{
    public static class JsonSerializerFactory
    {
       public static IJsonSerializer CreateJsonSerializer(bool ignoreIndentation = false) => new SystemTextJsonSerializer(
            new KebabCaseNamingPolicy(),
            new System.Text.Json.Serialization.JsonConverter[]
            {
                new _UrnJsonConverter(),
                new _DocflowContainingConverter()
            },
            ignoreIndentation
        );
    }
}