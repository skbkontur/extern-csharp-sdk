using System;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.ApiErrors;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.ApiTasks
{
    public class ApiTaskResult<TResult>
    {
        public static ApiTaskResult<TResult> Running(Guid id, Urn? taskType) => new(
            id,
            ApiTaskState.Running,
            taskType,
            null,
            default
        );
        
        public static ApiTaskResult<TResult> Success(TResult result, Guid id, Urn? taskType) => new(
            id,
            ApiTaskState.Succeed,
            taskType,
            null,
            result ?? throw new ArgumentNullException(nameof(result))
        );

        public static ApiTaskResult<TResult> TaskFailure(ApiError error, Guid id, Urn? taskType) => new(
            id,
            ApiTaskState.Failed,
            taskType,
            error ?? throw new ArgumentNullException(nameof(error)),
            default
        );

        private readonly TResult? successResult;
        private readonly ApiError? apiError;

        private ApiTaskResult(Guid id, ApiTaskState taskState, Urn? taskType, ApiError? apiError, TResult? successResult)
        {
            Id = id;
            TaskState = taskState;
            TaskType = taskType;
            this.apiError = apiError;
            this.successResult = successResult;
        }
        
        public Guid Id { get; }
        public ApiTaskState TaskState { get; }
        public Urn? TaskType { get; }

        public bool TryGetSuccessResult(out TResult result)
        {
            if (TaskState == ApiTaskState.Succeed)
            {
                result = successResult!;
                return true;
            }

            result = default!;
            return false;
        }

        public bool TryGetTaskError(out ApiError error)
        {
            if (TaskState == ApiTaskState.Failed)
            {
                error = apiError!;
                return true;
            }

            error = default!;
            return false;
        }

        public override string ToString() => 
            $"{nameof(TaskState)}: {TaskState}, {nameof(Id)}: {Id}, {nameof(TaskType)}: {TaskType}";

        public ApiTaskResult<TAnotherResult> Convert<TAnotherResult>(Func<TResult, TAnotherResult> converter) =>
            TaskState switch
            {
                ApiTaskState.Running => ApiTaskResult<TAnotherResult>.Running(Id, TaskType),
                ApiTaskState.Succeed => ApiTaskResult<TAnotherResult>.Success(converter(successResult!), Id, TaskType),
                ApiTaskState.Failed => ApiTaskResult<TAnotherResult>.TaskFailure(apiError!, Id, TaskType),
                _ => throw Errors.UnexpectedEnumMember(nameof(TaskState), TaskState)
            };
    }
}