using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Primitives
{
    public interface IEntityListSliceLoading<T> 
    {
        Task<Slice<T>> LoadSliceAsync(TimeSpan? timeout = null);
    }
}