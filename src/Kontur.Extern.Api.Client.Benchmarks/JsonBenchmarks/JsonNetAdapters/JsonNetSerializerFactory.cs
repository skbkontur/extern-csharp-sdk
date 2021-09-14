using Kontur.Extern.Api.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters.Converters;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.Docflows;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kontur.Extern.Api.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters
{
    public static class JsonNetSerializerFactory
    {
        public static IJsonSerializer CreateJsonSerializer(bool ignoreIndentation = false) => new JsonNetSerializer(
            new KebabCaseNamingStrategy(),
            new JsonConverter[]
            {
                new UrnJsonConverter(),
                new DocflowContainingConverter<IDocflowWithDocuments>(),
                new DocflowContainingConverter<IDocflow>()
            },
            ignoreIndentation
        );
    }
}