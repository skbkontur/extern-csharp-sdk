using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Primitives
{
    public interface IEntityListSlicing<T>
    {
        IEntityListSliceLoading<T> Skip(long skip);

        Task<IReadOnlyList<T>> LoadAllAsync(TimeSpan? timeout = null);
        Task<Slice<T>> LoadSliceAsync(TimeSpan? timeout = null);
    }
}