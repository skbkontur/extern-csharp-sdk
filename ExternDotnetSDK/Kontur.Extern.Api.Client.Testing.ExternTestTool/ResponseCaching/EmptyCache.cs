using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.ResponseCaching
{
    public class EmptyCache : IResponseCache
    {
        public Task<string?> TryGetAsync(string key) => Task.FromResult<string?>(null);

        public Task SetValueAsync(string key, string value) => Task.CompletedTask;
    }
}