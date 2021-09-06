using BenchmarkDotNet.Attributes;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Models.Docflows;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks
{
    [MemoryDiagnoser]
    public class JsonConverters_ArraySegmentDeserialization_Benchmark
    {
        private JsonConvertersBenchmarkContext context = null!;

        [GlobalSetup]
        public void Setup() => context = new JsonConvertersBenchmarkContext();

        [Benchmark(Baseline = true, OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public IDocflowWithDocuments Deserialize_SysTextJson() => context.SysSerializer.Deserialize<IDocflowWithDocuments>(context.JsonBytes);

        [Benchmark(OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public IDocflowWithDocuments Deserialize_JsonNet() => context.JsonNetSerializer.Deserialize<IDocflowWithDocuments>(context.JsonBytes);
    }
}