using Kontur.Extern.Client.ApiLevel.Json.Converters;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingStrategies;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Json
{
    public class JsonSerializerFactory
    {
        public IJsonSerializer CreateApiJsonSerializer(bool ignoreIndentation = false) => new JsonNetSerializer(
            new NamingStrategies.KebabCaseNamingStrategy(),
            new JsonConverter[]
            {
                new UrnJsonConverter(),
                new DocflowContainingConverter<Docflow>(),
                new DocflowContainingConverter<DocflowPageItem>()
            },
            ignoreIndentation
        );
        
        public IJsonSerializer _CreateJsonSerializer(bool ignoreIndentation = false) => new SystemTextJsonSerializer(
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