using Kontur.Extern.Client.ApiLevel.Json.Converters;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.ApiLevel.Json.NamingStrategies;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Kontur.Extern.Client.Http.Serialization;
using Newtonsoft.Json;
using JsonSerializer = Kontur.Extern.Client.Http.Serialization.JsonSerializer;

namespace Kontur.Extern.Client.ApiLevel.Json
{
    internal class JsonSerializerFactory
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

        public IJsonSerializer CreateDefaultJsonSerializer() => new JsonSerializer();
    }
}