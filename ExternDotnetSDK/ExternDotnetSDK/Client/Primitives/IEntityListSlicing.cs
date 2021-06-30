using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives
{
    public interface IEntityListSlicing<T>
    {
        IEntityListSliceLoading<T> Skip(uint skip);

        Task<IReadOnlyList<T>> LoadAllAsync();
        Task<IReadOnlyList<T>> LoadSliceAsync();
    }
}