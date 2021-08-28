using System.Threading.Tasks;

namespace Kontur.Extern.Client.Testing.ExternTestTool.ResponseCaching
{
    public interface IResponseCache
    {
        Task<string?> TryGetAsync(string key);
        Task SetValueAsync(string key, string value);
    }
}