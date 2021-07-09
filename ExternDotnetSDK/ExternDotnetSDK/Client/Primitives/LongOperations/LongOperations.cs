using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives.LongOperations
{
    public interface ILongOperationAwaiter<T>
    {
        Guid TaskId { get; }

        Task<T> WaitForCompletion(); // return result or throw exception (ApiException or OperationCancelledException)
    }
}