using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives.LongOperations
{
    public interface ILongOperation
    { 
        Task<ILongOperationAwaiter> StartAsync();
        
        ILongOperationAwaiter ContinueAwait(Guid taskId);

        Task<LongOperationStatus> CheckStatusAsync(Guid taskId);
    }
    
    public interface ILongOperation<TResult>
    { 
        Task<ILongOperationAwaiter<TResult>> StartAsync();
        
        ILongOperationAwaiter<TResult> ContinueAwait(Guid taskId);

        Task<LongOperationStatus<TResult>> CheckStatusAsync(Guid taskId);
    }
}