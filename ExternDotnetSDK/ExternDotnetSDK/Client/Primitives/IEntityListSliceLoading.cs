using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives
{
    public interface IEntityListSliceLoading<T> 
    {
        Task<(IReadOnlyList<T> Items, bool HasNextSlice)> LoadSliceAsync();
    }
}