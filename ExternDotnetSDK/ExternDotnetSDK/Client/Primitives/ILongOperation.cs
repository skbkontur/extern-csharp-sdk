using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;

namespace Kontur.Extern.Client.Primitives
{
    internal interface ILongOperation
    { 
        Task<ILongOperationAwaiter> StartAsync();
        
        ILongOperationAwaiter ContinueAwait(Guid taskId);

        Task<ApiTaskState> CheckStatusAsync(Guid taskId);
    }
}