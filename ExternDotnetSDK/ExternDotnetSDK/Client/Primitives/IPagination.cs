using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives
{
    public interface IPagination<T>
    {
        uint TotalItems { get; }
        uint CurrentPage { get; }
        uint PageSize { get; }
        uint TotalPages { get; }
        uint StartPage { get; }
        uint EndPage { get; }
        uint StartIndex { get; }
        uint EndIndex { get; }
        IEnumerable<uint> Pages { get; }
        
        Task<IReadOnlyList<T>> LoadPageAsync(uint pageIndex);
        
        // todo: consider to use IAsyncEnumerable<T>
        Task<IReadOnlyList<T>> AllAsync();
    }
}