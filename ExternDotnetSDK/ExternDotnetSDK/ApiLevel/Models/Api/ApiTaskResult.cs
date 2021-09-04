#nullable enable
using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using OneOf;

namespace Kontur.Extern.Client.ApiLevel.Models.Api
{
    [PublicAPI]
    public class ApiTaskResult<TResult>
    {
        public static ApiTaskResult<TResult> Running(Guid taskId, Urn? taskType) => new()
        {
            Id = taskId,
            TaskState = ApiTaskState.Running,
            TaskType = taskType
        };

        public static ApiTaskResult<TResult> Success(Guid taskId, Urn? taskType, TResult result) => new()
        {
            Id = taskId,
            TaskType = taskType,
            TaskState = ApiTaskState.Succeed,
            TaskResult = result
        };

        public static ApiTaskResult<TResult> Failure(Guid taskId, Urn? taskType, ApiError apiError) => new()
        {
            Id = taskId,
            TaskType = taskType,
            TaskState = ApiTaskState.Failed,
            ApiError = apiError
        };

        public Guid Id { get; set; }
        public ApiTaskState TaskState { get; set; }
        public Urn? TaskType { get; set; }
        public TResult? TaskResult { get; set; }
        public ApiError? ApiError { get; set; }

        public ApiTaskResult<TOtherResult> Convert<TOtherResult>(Func<TResult, TOtherResult> converter) => new()
        {
            ApiError = ApiError,
            Id = Id,
            TaskState = TaskState,
            TaskType = TaskType,
            TaskResult = TaskResult is null ? default : converter(TaskResult)
        };
    }
    
    public class ApiTaskResult<TResult1, TResult2> : ApiTaskResult<OneOf<TResult1, TResult2>>
    {
        public ApiTaskResult(ApiTaskResult<TResult1> apiTaskResult)
        {
            ApiError = apiTaskResult.ApiError;
            Id = apiTaskResult.Id;
            TaskState = apiTaskResult.TaskState;
            TaskType = apiTaskResult.TaskType;
            if (TaskState == ApiTaskState.Succeed && apiTaskResult.TaskResult is not null)
            {
                TaskResult = apiTaskResult.TaskResult;
            }
        }

        public ApiTaskResult(ApiTaskResult<TResult2> apiTaskResult)
        {
            ApiError = apiTaskResult.ApiError;
            Id = apiTaskResult.Id;
            TaskState = apiTaskResult.TaskState;
            TaskType = apiTaskResult.TaskType;
            if (TaskState == ApiTaskState.Succeed && apiTaskResult.TaskResult is not null)
            {
                TaskResult = apiTaskResult.TaskResult;
            }
        }

        private ApiTaskResult()
        {
        }

        public T GetResultAs<T>(Func<TResult1, T> firstResultConvert, Func<TResult2, T> secondResultConvert) => 
            TaskResult.Match(firstResultConvert, secondResultConvert);
        
        public ApiTaskResult<TResult1, T> ConvertSecondResult<T>(Func<TResult2, T> secondResultConvert) => 
            Convert(x => x, secondResultConvert);
        
        public ApiTaskResult<TResult1> ConsiderSecondAsError(Func<TResult2, ApiError> convertSecondResultToError)
        {
            var result = new ApiTaskResult<TResult1>
            {
                ApiError = ApiError,
                Id = Id,
                TaskState = TaskState,
                TaskType = TaskType
            };

            if (TaskState == ApiTaskState.Succeed)
            {
                if (TaskResult.IsT0)
                {
                    result.TaskResult = TaskResult.AsT0;
                }
                else
                {
                    result.TaskState = ApiTaskState.Failed;
                    result.ApiError = convertSecondResultToError(TaskResult.AsT1);
                }
            }

            return result;
        }

        public ApiTaskResult<TConverted1, TConverted2> Convert<TConverted1, TConverted2>(Func<TResult1, TConverted1> convertFirst, Func<TResult2, TConverted2> convertSecond) => new()
        {
            ApiError = ApiError,
            Id = Id,
            TaskState = TaskState,
            TaskType = TaskType,
            TaskResult = TaskState == ApiTaskState.Succeed
                ? TaskResult.IsT0 ? convertFirst(TaskResult.AsT0) : convertSecond(TaskResult.AsT1)
                : default
        };

        public static implicit operator ApiTaskResult<TResult1, TResult2>(ApiTaskResult<TResult1> apiTaskResult) => new(apiTaskResult);
        
        public static implicit operator ApiTaskResult<TResult1, TResult2>(ApiTaskResult<TResult2> apiTaskResult) => new(apiTaskResult);
    }

    public interface IApiTaskResult
    {
        bool IsEmpty { get; }
    }

    public class _ApiTaskResult<TResult, TFailureResult>
        where TResult : IApiTaskResult
        where TFailureResult : IApiTaskResult
    {
        public static _ApiTaskResult<TResult, TFailureResult> Running(Guid id, Urn? taskType) => new(
            id,
            ApiTaskState.Running,
            taskType,
            null,
            default,
            default
        );
        
        public static _ApiTaskResult<TResult, TFailureResult> Success(TResult result, Guid id, Urn? taskType)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            if (result.IsEmpty)
                throw Exceptions.Errors.CannotCreateApiTaskResultWithIsEmptyResult(nameof(result), result);
            
            return new(
                id,
                ApiTaskState.Succeed,
                taskType,
                null,
                result,
                default
            );
        }

        public static _ApiTaskResult<TResult, TFailureResult> FailureResult(TFailureResult result, Guid id, Urn? taskType)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            if (result.IsEmpty)
                throw Exceptions.Errors.CannotCreateApiTaskResultWithIsEmptyResult(nameof(result), result); 
            
            return new(
                id,
                ApiTaskState.Failed,
                taskType,
                null,
                default,
                result ?? throw new ArgumentNullException(nameof(result))
            );
        }

        public static _ApiTaskResult<TResult, TFailureResult> TaskFailure(ApiError error, Guid id, Urn? taskType) => new(
            id,
            ApiTaskState.Failed,
            taskType,
            error ?? throw new ArgumentNullException(nameof(error)),
            default,
            default
        );

        private readonly TResult? successResult;
        private readonly TFailureResult? failureResult;
        private readonly ApiError? apiError;

        public _ApiTaskResult(Guid id, ApiTaskState taskState, Urn? taskType, ApiError? apiError, TResult? successResult, TFailureResult? failureResult)
        {
            Id = id;
            TaskState = taskState;
            TaskType = taskType;
            this.apiError = apiError;
            this.successResult = successResult;
            this.failureResult = failureResult;
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
        
        public bool TryGetFailureResult(out TFailureResult result)
        {
            if (TaskState == ApiTaskState.Failed && apiError == null)
            {
                result = failureResult!;
                return true;
            }

            result = default!;
            return false;
        }

        public bool TryGetTaskError(out ApiError error)
        {
            if (TaskState == ApiTaskState.Failed && apiError != null)
            {
                error = apiError!;
                return true;
            }

            error = default!;
            return false;
        }

        public _ApiTaskResult<TAnotherResult> ConvertTo<TAnotherResult>(
            Func<TResult, TAnotherResult> successResultConverter,
            Func<TFailureResult, ApiError> failureResultConverter) =>
            TaskState switch
            {
                ApiTaskState.Running => 
                    _ApiTaskResult<TAnotherResult>.Running(Id, TaskType),
                ApiTaskState.Succeed => 
                    _ApiTaskResult<TAnotherResult>.Success(successResultConverter(successResult!), Id, TaskType),
                ApiTaskState.Failed when apiError == null => 
                    _ApiTaskResult<TAnotherResult>.TaskFailure(failureResultConverter(failureResult!), Id, TaskType),
                ApiTaskState.Failed when apiError != null => 
                    _ApiTaskResult<TAnotherResult>.TaskFailure(apiError, Id, TaskType),
                _ => throw Exceptions.Errors.UnexpectedEnumMember(nameof(TaskState), TaskState)
            };

        public override string ToString() => 
            $"{nameof(TaskState)}: {TaskState}, {nameof(Id)}: {Id}, {nameof(TaskType)}: {TaskType}";
    }
    
    public class _ApiTaskResult<TResult>
    {
        public static _ApiTaskResult<TResult> Running(Guid id, Urn? taskType) => new(
            id,
            ApiTaskState.Running,
            taskType,
            null,
            default
        );
        
        public static _ApiTaskResult<TResult> Success(TResult result, Guid id, Urn? taskType) => new(
            id,
            ApiTaskState.Succeed,
            taskType,
            null,
            result ?? throw new ArgumentNullException(nameof(result))
        );

        public static _ApiTaskResult<TResult> TaskFailure(ApiError error, Guid id, Urn? taskType) => new(
            id,
            ApiTaskState.Failed,
            taskType,
            error ?? throw new ArgumentNullException(nameof(error)),
            default
        );

        private readonly TResult? successResult;
        private readonly ApiError? apiError;

        public _ApiTaskResult(Guid id, ApiTaskState taskState, Urn? taskType, ApiError? apiError, TResult? successResult)
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
    }
}