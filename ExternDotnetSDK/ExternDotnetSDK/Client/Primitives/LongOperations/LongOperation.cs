#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Primitives.Polling;

namespace Kontur.Extern.Client.Primitives.LongOperations
{
    internal class LongOperation : ILongOperation
    {
        private readonly Func<Guid, Task<LongOperationStatus>> checkStatusAsync;
        private readonly Func<Task<(Guid id, LongOperationStatus Status)>> startAsync;
        private readonly IPollingStrategy pollingStrategy;

        public LongOperation(
            Func<Task<(Guid id, LongOperationStatus Status)>> startAsync, 
            Func<Guid, Task<LongOperationStatus>> checkStatusAsync, 
            IPollingStrategy pollingStrategy)
        {
            this.startAsync = startAsync;
            this.checkStatusAsync = checkStatusAsync;
            this.pollingStrategy = pollingStrategy;
        }

        public async Task<ILongOperationAwaiter> StartAsync()
        {
            var (id, status) = await startAsync().ConfigureAwait(false);
            return status.EnsureSuccess().IsCompleted 
                ? new AlreadyCompletedAwaiter(id) 
                : ContinueAwait(id);
        }

        public ILongOperationAwaiter ContinueAwait(Guid taskId) => new LongOperationAwaiter(taskId, checkStatusAsync, pollingStrategy);

        public Task<LongOperationStatus> CheckStatusAsync(Guid taskId) => checkStatusAsync(taskId);

        private class LongOperationAwaiter : ILongOperationAwaiter
        {
            private readonly Func<Guid, Task<LongOperationStatus>> checkStatusAsync;
            private readonly IPollingStrategy pollingStrategy;

            public LongOperationAwaiter(Guid taskId, Func<Guid, Task<LongOperationStatus>> checkStatusAsync, IPollingStrategy pollingStrategy)
            {
                this.checkStatusAsync = checkStatusAsync;
                this.pollingStrategy = pollingStrategy;
                TaskId = taskId;
            }
            
            public Guid TaskId { get; }

            public async Task WaitForCompletion()
            {
                var polling = pollingStrategy.Start();
                while (true)
                {
                    var status = await checkStatusAsync(TaskId).ConfigureAwait(false);
                    if (status.EnsureSuccess().IsCompleted)
                        return;
                    
                    await polling.WaitForNextAttempt().ConfigureAwait(false);
                }
            }
        }
        
        private class AlreadyCompletedAwaiter : ILongOperationAwaiter
        {
            public AlreadyCompletedAwaiter(Guid taskId) => TaskId = taskId;

            public Guid TaskId { get; }

            public Task WaitForCompletion() => Task.CompletedTask;
        }
    }
}