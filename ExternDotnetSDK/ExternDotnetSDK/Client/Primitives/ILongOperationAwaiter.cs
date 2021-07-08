using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives
{
    public interface ILongOperationAwaiter
    {
        Guid TaskId { get; }
        Task WaitForCompletion();
        
        // NOTE: this method here only to show possible extensibility for the interface
        IObservable<LongOperationStatus> Observe();
    }
}