using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Primitives.LongOperations
{
    [PublicAPI]
    public interface ILongOperationAwaiter<TResult>
    {
        Guid TaskId { get; }

        Task<TResult> WaitForCompletion(); // return result or throw exception (ApiException or OperationCancelledException)
    }
    
    [PublicAPI]
    public interface ILongOperationAwaiter<TResult, TFailure>
        where TFailure : ILongOperationFailure
    {
        Guid TaskId { get; }

        /// <summary>
        /// return result or throw exception (ApiException or OperationCancelledException)
        /// </summary>
        /// <returns></returns>
        Task<TResult> WaitForCompletion();

        Task<LongOperationResult<TResult, TFailure>> WaitForSuccessOrFailure(); 
    }
}