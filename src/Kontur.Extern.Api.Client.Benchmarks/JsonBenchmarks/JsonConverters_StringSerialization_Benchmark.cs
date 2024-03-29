using BenchmarkDotNet.Attributes;

namespace Kontur.Extern.Api.Client.Benchmarks.JsonBenchmarks
{
    [MemoryDiagnoser]
    public class JsonConverters_StringSerialization_Benchmark
    {
        private JsonConvertersBenchmarkContext context = null!;

        [GlobalSetup]
        public void Setup() => context = new JsonConvertersBenchmarkContext();

        [Benchmark(Baseline = true, OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public string Serialize_SysTextJson() => context.SysSerializer.SerializeToIndentedString(context.Docflow);

        [Benchmark(OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public string Serialize_JsonNet() => context.JsonNetSerializer.SerializeToIndentedString(context.Docflow);
    }
}