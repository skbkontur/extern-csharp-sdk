using BenchmarkDotNet.Attributes;
using Kontur.Extern.Client.ApiLevel.Json;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Http.Serialization;

namespace Kontur.Extern.Client.Benchmarks
{
    [MemoryDiagnoser]
    public class JsonConverters_Deserialization_Benchmark
    {
        private const int OperationsPerInvoke = 100;
        private JsonConvertersBenchmarkContext context;

        [GlobalSetup]
        public void Setup() => context = new JsonConvertersBenchmarkContext();

        [Benchmark(Baseline = true, OperationsPerInvoke = OperationsPerInvoke)]
        public Docflow Deserialize_SysTextJson() => context.SysSerializer.DeserializeFromJson<Docflow>(context.Json);

        [Benchmark(OperationsPerInvoke = OperationsPerInvoke)]
        public Docflow Deserialize_JsonNet() => context.JsonNetSerializer.DeserializeFromJson<Docflow>(context.Json);
    }
}