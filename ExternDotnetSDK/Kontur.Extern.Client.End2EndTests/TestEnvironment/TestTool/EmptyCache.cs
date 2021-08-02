using System.Threading.Tasks;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool
{
    internal class EmptyCache : IResponseCache
    {
        public Task<string?> TryGetAsync(string key) => Task.FromResult<string?>(null);

        public Task SetValueAsync(string key, string value) => Task.CompletedTask;
    }
}