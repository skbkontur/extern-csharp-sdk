using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives
{
    internal interface ILongOperationAwaiter<T>
    {
        Guid TaskId { get; }
        Task<T> WaitForCompletion(); // return result or throw exception (ApiException or OperationCancelledException)
        
        // NOTE: this method here only to show possible extensibility for the interface
        IObservable<LongOperationStatus<T>> Observe();
    }
    
    internal class LongOperationStatus<T> : LongOperationStatus
    {
        private readonly T result;

        public LongOperationStatus(T result) => this.result = result;

        public bool TryGetResult(out T result)
        {
            if (IsCompleted)
            {
                result = this.result;
                return true;
            }

            result = default;
            return false;
        }
    }
}