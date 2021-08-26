using BenchmarkDotNet.Attributes;

namespace Kontur.Extern.Client.Benchmarks
{
    [MemoryDiagnoser]
    public class JsonConverters_Serialization_Benchmark
    {
        private const int OperationsPerInvoke = 100;
        private JsonConvertersBenchmarkContext context;

        [GlobalSetup]
        public void Setup() => context = new JsonConvertersBenchmarkContext();

        [Benchmark(Baseline = true, OperationsPerInvoke = OperationsPerInvoke)]
        public string Serialize_SysTextJson() => context.SysSerializer.SerializeToIndentedString(context.Docflow);

        [Benchmark(OperationsPerInvoke = OperationsPerInvoke)]
        public string Serialize_JsonNet() => context.JsonNetSerializer.SerializeToIndentedString(context.Docflow);
    }
}