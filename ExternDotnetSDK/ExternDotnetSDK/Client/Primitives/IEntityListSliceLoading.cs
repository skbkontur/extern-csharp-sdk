using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Primitives
{
    public interface IEntityListSliceLoading<T> 
    {
        Task<(IReadOnlyList<T> Items, bool HasNextSlice)> LoadSliceAsync(TimeSpan? timeout = null);
    }
}