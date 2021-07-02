using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives
{
    internal delegate Task<(IReadOnlyList<T> Items, long totalItems)> GetSliceAsync<T>(long skip, long take);
}