using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Primitives.LongOperations
{
    [PublicAPI]
    public interface ILongOperationAwaiter<T>
    {
        Guid TaskId { get; }

        Task<T> WaitForCompletion(); // return result or throw exception (ApiException or OperationCancelledException)
    }
}