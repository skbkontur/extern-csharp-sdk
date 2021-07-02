using System.Collections.Generic;

namespace Kontur.Extern.Client.Primitives
{
    public interface IPage
    {
        bool IsPageNonExistent { get; }
        long TotalItems { get; }
        long CurrentPage { get; }
        uint PageSize { get; }
        long TotalPages { get; }
        IEnumerable<long> Pages { get; }
    }
    
    public interface IPage<out T> : IPage, IReadOnlyList<T>
    {
    }
}