#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Primitives.Polling;

namespace Kontur.Extern.Api.Client.Primitives.LongOperations
{
    internal class LongOperation<T> : ILongOperation<T>
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
            if (startResult.TryGetSuccessResult(out var successResult))
            {
                return new AlreadyCompletedAwaiter(startResult.Id, successResult);
            }

            if (startResult.TryGetTaskError(out var apiError))
            {
                throw Errors.LongOperationFailed(apiError);
            }

            return ContinueAwait(startResult.Id);
        }

        public ILongOperationAwaiter<T> ContinueAwait(Guid taskId) => new LongOperationAwaiter(taskId, checkStatusAsync, pollingStrategy);

        public async Task<LongOperationStatus<T>> CheckStatusAsync(Guid taskId)
        {
            var taskResult = await checkStatusAsync(taskId).ConfigureAwait(false);
            
            if (taskResult.TryGetSuccessResult(out var successResult))
            {
                return LongOperationStatus<T>.Completed(successResult);
            }

            if (taskResult.TryGetTaskError(out var apiError))
            {
                return LongOperationStatus<T>.Failed(apiError);
            }

            return LongOperationStatus<T>.InProgress;
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
                    if (taskResult.TryGetSuccessResult(out var successResult))
                    {
                        return successResult;
                    }

                    if (taskResult.TryGetTaskError(out var apiError))
                    {
                        throw Errors.LongOperationFailed(apiError);
                    }

                    await polling.WaitForNextAttempt().ConfigureAwait(false);
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

    internal class LongOperation<TResult, TFailure> : ILongOperation<TResult, TFailure>
        where TFailure : ILongOperationFailure, IApiTaskResult
        where TResult : IApiTaskResult
    {
        private readonly Func<Guid, Task<ApiTaskResult<TResult, TFailure>>> checkStatusAsync;
        private readonly Func<Task<ApiTaskResult<TResult, TFailure>>> startAsync;
        private readonly IPollingStrategy pollingStrategy;

        public LongOperation(Func<Task<ApiTaskResult<TResult, TFailure>>> startAsync, 
                             Func<Guid, Task<ApiTaskResult<TResult, TFailure>>> checkStatusAsync, 
                             IPollingStrategy pollingStrategy)
        {
            this.startAsync = startAsync;
            this.checkStatusAsync = checkStatusAsync;
            this.pollingStrategy = pollingStrategy;
        }

        public async Task<ILongOperationAwaiter<TResult, TFailure>> StartAsync()
        {
            var taskResult = await startAsync().ConfigureAwait(false);
            var longOperationResult = ToLongOperationResult(taskResult);
            if (longOperationResult.HasValue)
                return new AlreadyCompletedAwaiter(taskResult.Id, longOperationResult.Value);

            return ContinueAwait(taskResult.Id);
        }

        public ILongOperationAwaiter<TResult, TFailure> ContinueAwait(Guid taskId) => 
            new LongOperationAwaiter(taskId, checkStatusAsync, pollingStrategy);
        
        public async Task<LongOperationStatus<TResult, TFailure>> CheckStatusAsync(Guid taskId)
        {
            var taskResult = await checkStatusAsync(taskId).ConfigureAwait(false);
            if (taskResult.TryGetSuccessResult(out var successResult))
                return LongOperationStatus<TResult, TFailure>.Completed(successResult);

            if (taskResult.TryGetFailureResult(out var failureResult))
                return LongOperationStatus<TResult, TFailure>.Failure(failureResult);

            if (taskResult.TryGetTaskError(out var apiError))
                return LongOperationStatus<TResult, TFailure>.Failed(apiError);

            return LongOperationStatus<TResult, TFailure>.InProgress;
        }

        private static LongOperationResult<TResult, TFailure>? ToLongOperationResult(ApiTaskResult<TResult, TFailure> taskResult)
        {
            if (taskResult.TryGetSuccessResult(out var successResult))
            {
                return LongOperationResult<TResult, TFailure>.Success(successResult);
            }

            if (taskResult.TryGetFailureResult(out var failureResult))
            {
                return LongOperationResult<TResult, TFailure>.Failure(failureResult);
            }

            if (taskResult.TryGetTaskError(out var apiError))
            {
                throw Errors.LongOperationFailed(apiError);
            }

            return null;
        }

        private abstract class LongOperationAwaiterBase : ILongOperationAwaiter<TResult, TFailure>
        {
            protected LongOperationAwaiterBase(Guid taskId) => TaskId = taskId;

            public Guid TaskId { get; }

            public async Task<TResult> WaitForCompletion()
            {
                var result = await WaitForSuccessOrFailure().ConfigureAwait(false);
                return result.GetSuccessResult();
            }

            public abstract Task<LongOperationResult<TResult, TFailure>> WaitForSuccessOrFailure();
        }

        private class LongOperationAwaiter : LongOperationAwaiterBase
        {
            private readonly Func<Guid, Task<ApiTaskResult<TResult, TFailure>>> checkStatusAsync;
            private readonly IPollingStrategy pollingStrategy;

            public LongOperationAwaiter(Guid taskId, 
                                        Func<Guid, Task<ApiTaskResult<TResult, TFailure>>> checkStatusAsync, 
                                        IPollingStrategy pollingStrategy)
                : base(taskId)
            {
                this.checkStatusAsync = checkStatusAsync;
                this.pollingStrategy = pollingStrategy;
            }

            public override async Task<LongOperationResult<TResult, TFailure>> WaitForSuccessOrFailure()
            {
                var polling = pollingStrategy.Start();
                while (true)
                {
                    var taskResult = await checkStatusAsync(TaskId).ConfigureAwait(false);
                    var longOperationResult = ToLongOperationResult(taskResult);
                    if (longOperationResult.HasValue)
                        return longOperationResult.Value;
                    
                    await polling.WaitForNextAttempt().ConfigureAwait(false);
                }
            }
        }
        
        private class AlreadyCompletedAwaiter : LongOperationAwaiterBase
        {
            private readonly LongOperationResult<TResult, TFailure> operationResult;

            public AlreadyCompletedAwaiter(Guid taskId, LongOperationResult<TResult, TFailure> operationResult)
                : base(taskId)
            {
                this.operationResult = operationResult;
            }

            public override Task<LongOperationResult<TResult, TFailure>> WaitForSuccessOrFailure() => 
                Task.FromResult(operationResult);
        }
    }
}