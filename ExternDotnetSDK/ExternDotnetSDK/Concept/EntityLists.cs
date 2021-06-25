using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kontur.Extern.Client
{
    interface IEntityList<T>
    {
        IEntityList<T> Skip(uint skip);
        /// <summary>
        /// Allows to change take items (and page size)
        /// </summary>
        /// <param name="take"></param>
        /// <returns></returns>
        IEntityList<T> Take(uint take);
        Task<IReadOnlyList<T>> LoadAsync();
        
        /// <summary>
        /// return pagination interface
        /// </summary>
        /// <returns></returns>
        IPagination<T> Paging();

        Task<IReadOnlyList<T>> AllAsync();
    }

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