using System;
using BenchmarkDotNet.Attributes;

namespace Kontur.Extern.Api.Client.Benchmarks.JsonBenchmarks
{
    [MemoryDiagnoser]
    public class JsonConverters_ArraySegmentSerialization_Benchmark
    {
        private JsonConvertersBenchmarkContext context = null!;

        [GlobalSetup]
        public void Setup() => context = new JsonConvertersBenchmarkContext();

        [Benchmark(Baseline = true, OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public ArraySegment<byte> Serialize_SysTextJson() => context.SysSerializer.SerializeToJsonBytes(context.Docflow);

        [Benchmark(OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public ArraySegment<byte> Serialize_JsonNet() => context.JsonNetSerializer.SerializeToJsonBytes(context.Docflow);
    }
}