using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks
{
    [MemoryDiagnoser]
    public class JsonConverters_StreamDeserialization_Benchmark
    {
        private JsonConvertersBenchmarkContext context = null!;

        [GlobalSetup]
        public void Setup() => context = new JsonConvertersBenchmarkContext();

        [Benchmark(Baseline = true, OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public async Task<Docflow?> Deserialize_SysTextJson() => await context.SysSerializer.DeserializeAsync<Docflow>(context.JsonStream).ConfigureAwait(false);

        [Benchmark(OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public async Task<Docflow?> Deserialize_JsonNet() => await context.JsonNetSerializer.DeserializeAsync<Docflow>(context.JsonStream).ConfigureAwait(false);
    }
}