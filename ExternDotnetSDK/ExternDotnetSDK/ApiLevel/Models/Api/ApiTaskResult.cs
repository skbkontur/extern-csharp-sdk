#nullable enable
using System;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Errors;

namespace Kontur.Extern.Client.ApiLevel.Models.Api
{
    public class ApiTaskResult<TResult, TFailureResult>
        where TResult : IApiTaskResult
        where TFailureResult : IApiTaskResult
    {
        public static ApiTaskResult<TResult, TFailureResult> Running(Guid id, Urn? taskType) => new(
            id,
            ApiTaskState.Running,
            taskType,
            null,
            default,
            default
        );
        
        public static ApiTaskResult<TResult, TFailureResult> Success(TResult result, Guid id, Urn? taskType)
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

        public static ApiTaskResult<TResult, TFailureResult> FailureResult(TFailureResult result, Guid id, Urn? taskType)
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

        public static ApiTaskResult<TResult, TFailureResult> TaskFailure(ApiError error, Guid id, Urn? taskType) => new(
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

        private ApiTaskResult(Guid id, ApiTaskState taskState, Urn? taskType, ApiError? apiError, TResult? successResult, TFailureResult? failureResult)
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

        public ApiTaskResult<TResult> ConvertToSingleApiResult(Func<TFailureResult, ApiError> failureResultConverter) => 
            ConvertToSingleApiResult(x => x, failureResultConverter);
        
        public ApiTaskResult<TAnotherResult> ConvertToSingleApiResult<TAnotherResult>(
            Func<TResult, TAnotherResult> successResultConverter,
            Func<TFailureResult, ApiError> failureResultConverter) =>
            TaskState switch
            {
                ApiTaskState.Running => 
                    ApiTaskResult<TAnotherResult>.Running(Id, TaskType),
                ApiTaskState.Succeed => 
                    ApiTaskResult<TAnotherResult>.Success(successResultConverter(successResult!), Id, TaskType),
                ApiTaskState.Failed when apiError == null => 
                    ApiTaskResult<TAnotherResult>.TaskFailure(failureResultConverter(failureResult!), Id, TaskType),
                ApiTaskState.Failed when apiError != null => 
                    ApiTaskResult<TAnotherResult>.TaskFailure(apiError, Id, TaskType),
                _ => throw Exceptions.Errors.UnexpectedEnumMember(nameof(TaskState), TaskState)
            };

        public ApiTaskResult<TResult, TAnotherFailure> ConvertFailureResult<TAnotherFailure>(
            Func<TFailureResult, TAnotherFailure> failureResultConverter)
            where TAnotherFailure : IApiTaskResult
        {
            return TaskState switch
            {
                ApiTaskState.Running =>
                    ApiTaskResult<TResult, TAnotherFailure>.Running(Id, TaskType),
                ApiTaskState.Succeed =>
                    ApiTaskResult<TResult, TAnotherFailure>.Success(successResult!, Id, TaskType),
                ApiTaskState.Failed when apiError == null =>
                    ApiTaskResult<TResult, TAnotherFailure>.FailureResult(failureResultConverter(failureResult!), Id, TaskType),
                ApiTaskState.Failed when apiError != null =>
                    ApiTaskResult<TResult, TAnotherFailure>.TaskFailure(apiError, Id, TaskType),
                _ => throw Exceptions.Errors.UnexpectedEnumMember(nameof(TaskState), TaskState)
            };
        }

        public override string ToString() => 
            $"{nameof(TaskState)}: {TaskState}, {nameof(Id)}: {Id}, {nameof(TaskType)}: {TaskType}";
    }
    
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
                _ => throw Exceptions.Errors.UnexpectedEnumMember(nameof(TaskState), TaskState)
            };
    }
}