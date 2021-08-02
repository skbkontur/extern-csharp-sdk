using System.Threading.Tasks;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool
{
    internal interface IResponseCache
    {
        Task<string?> TryGetAsync(string key);
        Task SetValueAsync(string key, string value);
    }
}