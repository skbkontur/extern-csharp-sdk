using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Primitives
{
    public interface IPagination<T>
    {
        Task<Page<T>> LoadPageAsync(long pageIndex, TimeSpan? timeout = null);
        
        // todo: consider to use IAsyncEnumerable<T>
        Task<IReadOnlyList<T>> LoadAllAsync(TimeSpan? timeout = null);
    }
}