using BenchmarkDotNet.Attributes;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks
{
    [MemoryDiagnoser]
    public class JsonConverters_StreamSerialization_Benchmark
    {
        private JsonConvertersBenchmarkContext context = null!;

        [GlobalSetup]
        public void Setup() => context = new JsonConvertersBenchmarkContext();

        [Benchmark(Baseline = true, OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public void Serialize_SysTextJson() => context.SysSerializer.SerializeToJsonStreamAsync(context.Docflow, context.TargetStream);

        [Benchmark(OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public void Serialize_JsonNet() => context.JsonNetSerializer.SerializeToJsonStreamAsync(context.Docflow, context.TargetStream);
    }
}