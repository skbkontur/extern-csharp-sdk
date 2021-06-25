using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Api.Enums;

namespace Kontur.Extern.Client
{
    internal interface ILongOperation
    { 
        Task<ILongOperationAwaiter> StartAsync();
        
        ILongOperationAwaiter ContinueAwait(Guid taskId);

        Task<ApiTaskState> CheckStatusAsync(Guid taskId);
    }
    
    internal interface ILongOperationAwaiter
    {
        Guid TaskId { get; }
        Task WaitForCompletion();
        
        // NOTE: this method here only to show possible extensibility for the interface
        IObservable<LongOperationStatus> Observe();
    }

    internal class LongOperationStatus
    {
        public bool IsCompleted { get; }
        public Progress Progress { get; }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Progress
    {
        public double CurrentValue { get; }
        public double MaxValue { get; }
        public double Percent { get; }
    } 

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