using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Models.Docflows;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks
{
    [MemoryDiagnoser]
    public class JsonConverters_StreamDeserialization_Benchmark
    {
        private JsonConvertersBenchmarkContext context = null!;

        [GlobalSetup]
        public void Setup() => context = new JsonConvertersBenchmarkContext();

        [Benchmark(Baseline = true, OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public async Task<IDocflowWithDocuments> Deserialize_SysTextJson() => await context.SysSerializer.DeserializeAsync<IDocflowWithDocuments>(context.JsonStream).ConfigureAwait(false);

        [Benchmark(OperationsPerInvoke = JsonConvertersBenchmarkContext.OperationsPerInvoke)]
        public async Task<IDocflowWithDocuments> Deserialize_JsonNet() => await context.JsonNetSerializer.DeserializeAsync<IDocflowWithDocuments>(context.JsonStream).ConfigureAwait(false);
    }
}