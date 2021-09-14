using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Primitives.LongOperations
{
    public interface ILongOperationAwaiter
    {
        Guid TaskId { get; }
        Task WaitForCompletion();
    }
}