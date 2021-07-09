using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Api;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Primitives.Polling;

namespace Kontur.Extern.Client.Primitives.LongOperations
{
    internal class LongOperation<T> : ILongOperation<T>
        where T : class
    {
        private readonly Func<Guid, Task<ApiTaskResult<T>>> checkStatusAsync;
        private readonly Func<Task<ApiTaskResult<T>>> startAsync;
        private readonly IPollingStrategy pollingStrategy;

        public LongOperation(Func<Task<ApiTaskResult<T>>> startAsync, Func<Guid, Task<ApiTaskResult<T>>> checkStatusAsync, IPollingStrategy pollingStrategy)
        {
            this.startAsync = startAsync;
            this.checkStatusAsync = checkStatusAsync;
            this.pollingStrategy = pollingStrategy;
        }

        public async Task<ILongOperationAwaiter<T>> StartAsync()
        {
            var startResult = await startAsync().ConfigureAwait(false);
            return startResult.TaskState switch
            {
                ApiTaskState.Failed => throw Errors.LongOperationFailed(startResult.Error),
                ApiTaskState.Succeed => new AlreadyCompletedAwaiter(startResult.Id, startResult.TaskResult),
                _ => ContinueAwait(startResult.Id)
            };
        }

        public ILongOperationAwaiter<T> ContinueAwait(Guid taskId) => new LongOperationAwaiter(taskId, checkStatusAsync, pollingStrategy);

        public async Task<LongOperationStatus<T>> CheckStatusAsync(Guid taskId)
        {
            var taskResult = await checkStatusAsync(taskId).ConfigureAwait(false);
            return taskResult.TaskState switch
            {
                ApiTaskState.Running => LongOperationStatus<T>.InProgress,
                ApiTaskState.Succeed => LongOperationStatus<T>.Completed(taskResult.TaskResult),
                ApiTaskState.Failed => LongOperationStatus<T>.Failed(taskResult.Error),
                _ => throw Errors.UnexpectedEnumMember(nameof(taskResult.TaskState), taskResult.TaskState)
            };
        }

        private class LongOperationAwaiter : ILongOperationAwaiter<T>
        {
            private readonly Func<Guid, Task<ApiTaskResult<T>>> checkStatusAsync;
            private readonly IPollingStrategy pollingStrategy;

            public LongOperationAwaiter(Guid taskId, Func<Guid, Task<ApiTaskResult<T>>> checkStatusAsync, IPollingStrategy pollingStrategy)
            {
                this.checkStatusAsync = checkStatusAsync;
                this.pollingStrategy = pollingStrategy;
                TaskId = taskId;
            }
            
            public Guid TaskId { get; }

            public async Task<T> WaitForCompletion()
            {
                var polling = pollingStrategy.Start();
                while (true)
                {
                    var taskResult = await checkStatusAsync(TaskId).ConfigureAwait(false);
                    switch (taskResult.TaskState)
                    {
                        case ApiTaskState.Running:
                            await polling.WaitForNextAttempt().ConfigureAwait(false);
                            continue;
                        case ApiTaskState.Succeed:
                            return taskResult.TaskResult;
                        case ApiTaskState.Failed:
                            throw Errors.LongOperationFailed(taskResult.Error);
                        default:
                            throw Errors.UnexpectedEnumMember(nameof(taskResult.TaskState), taskResult.TaskState);
                    }
                }
            }
        }
        
        private class AlreadyCompletedAwaiter : ILongOperationAwaiter<T>
        {
            private readonly T taskResult;

            public AlreadyCompletedAwaiter(Guid taskId, T taskResult)
            {
                TaskId = taskId;
                this.taskResult = taskResult;
            }

            public Guid TaskId { get; }

            public Task<T> WaitForCompletion() => Task.FromResult(taskResult);
        }
    }
}