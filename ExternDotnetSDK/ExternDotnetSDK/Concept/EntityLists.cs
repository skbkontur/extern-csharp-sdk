using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kontur.Extern.Client
{
    interface IEntityList<T>
    {
        
        /// <summary>
        /// Allows to change take items (and page size)
        /// </summary>
        /// <param name="take"></param>
        /// <returns></returns>
        IEntityListSlicing<T> SliceBy(uint take);
        
        /// <summary>
        /// return pagination interface
        /// </summary>
        /// <returns></returns>
        IPagination<T> Paging(uint pageSize);        
    }

    public interface IEntityListSlicing<T>
    {
        IEntityListSliceLoading<T> Skip(uint skip);

        Task<IReadOnlyList<T>> LoadAllAsync();
        Task<IReadOnlyList<T>> LoadSliceAsync();
    }

    public interface IEntityListSliceLoading<T> 
    {
        Task<(IReadOnlyList<T> Items, bool HasNextSlice)> LoadSliceAsync();
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