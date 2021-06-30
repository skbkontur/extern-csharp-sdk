using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;

namespace Kontur.Extern.Client
{
    internal interface IDeferredOperation
    { 
        Task<IDeferred> StartAsync();
        
        IDeferred ContinueAwait(Guid taskId);

        Task<ApiTaskState> CheckStatusAsync(Guid taskId);
    }
    
    internal interface IDeferred
    {
        Guid TaskId { get; }
        Task WaitForCompletion();
        
        // NOTE: this method here only to show possible extensibility for the interface
        IObservable<DeferredProcessStatus> Observe();
    }

    internal class DeferredProcessStatus
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

    internal interface IDeferred<T>
    {
        Guid TaskId { get; }
        Task<T> WaitForCompletion(); // return result or throw exception (ApiException or OperationCancelledException)
        
        // NOTE: this method here only to show possible extensibility for the interface
        IObservable<DeferredProcessStatus<T>> Observe();
    }
    
    internal class DeferredProcessStatus<T> : DeferredProcessStatus
    {
        private readonly T result;

        public DeferredProcessStatus(T result) => this.result = result;

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