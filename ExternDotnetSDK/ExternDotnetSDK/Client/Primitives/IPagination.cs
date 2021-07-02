using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives
{
    public interface IPagination<T>
    {
        Task<Page<T>> LoadPageAsync(long pageIndex);
        
        // todo: consider to use IAsyncEnumerable<T>
        Task<IReadOnlyList<T>> LoadAllAsync();
    }
}