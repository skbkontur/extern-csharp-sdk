using Kontur.Extern.Client.ApiLevel.Json.Converters;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.ApiLevel.Json.NamingStrategies;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Http.Serialization;
using Newtonsoft.Json;
using JsonSerializer = Kontur.Extern.Client.Http.Serialization.JsonSerializer;

namespace Kontur.Extern.Client.ApiLevel.Json
{
    public class JsonSerializerFactory
    {
        public IJsonSerializer CreateApiJsonSerializer() => new JsonSerializer(
            new KebabCaseNamingStrategy(),
            new JsonConverter[]
            {
                new UrnJsonConverter(),
                new DocflowContainingConverter<Docflow>(),
                new DocflowContainingConverter<DocflowPageItem>()
            }
        );
        
        public IJsonSerializer _CreateApiJsonSerializer() => new SystemTextJsonSerializer(
            new _KebabCaseNamingStrategy(),
            new System.Text.Json.Serialization.JsonConverter[]
            {
                new _UrnJsonConverter(),
                new _DocflowContainingConverter()
            }
        );

        public IJsonSerializer CreateDefaultJsonSerializer() => new JsonSerializer();
    }
}