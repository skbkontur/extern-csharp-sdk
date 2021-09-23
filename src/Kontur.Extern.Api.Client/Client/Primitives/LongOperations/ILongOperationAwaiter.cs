using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Primitives.LongOperations
{
    [PublicAPI]
    public interface ILongOperationAwaiter
    {
        Guid TaskId { get; }
        Task WaitForCompletion();
    }
}