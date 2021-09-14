using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Primitives.LongOperations
{
    [PublicAPI]
    public interface ILongOperation
    { 
        Task<ILongOperationAwaiter> StartAsync();
        
        ILongOperationAwaiter ContinueAwait(Guid taskId);

        Task<LongOperationStatus> CheckStatusAsync(Guid taskId);
    }
    
    [PublicAPI]
    public interface ILongOperation<TResult>
    { 
        Task<ILongOperationAwaiter<TResult>> StartAsync();
        
        ILongOperationAwaiter<TResult> ContinueAwait(Guid taskId);

        Task<LongOperationStatus<TResult>> CheckStatusAsync(Guid taskId);
    }
    
    [PublicAPI]
    public interface ILongOperation<TResult, TFailure>
        where TFailure : ILongOperationFailure
    { 
        Task<ILongOperationAwaiter<TResult, TFailure>> StartAsync();
        
        ILongOperationAwaiter<TResult, TFailure> ContinueAwait(Guid taskId);

        Task<LongOperationStatus<TResult, TFailure>> CheckStatusAsync(Guid taskId);
    }
}