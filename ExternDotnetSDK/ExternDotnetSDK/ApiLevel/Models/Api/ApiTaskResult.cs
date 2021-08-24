#nullable enable
using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;
using OneOf;

namespace Kontur.Extern.Client.ApiLevel.Models.Api
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class ApiTaskResult<TResult>
    {
        public static ApiTaskResult<TResult> Running(Guid taskId, Urn taskType) => new()
        {
            Id = taskId,
            TaskState = ApiTaskState.Running,
            TaskType = taskType
        };

        public static ApiTaskResult<TResult> Success(Guid taskId, Urn taskType, TResult result) => new()
        {
            Id = taskId,
            TaskType = taskType,
            TaskState = ApiTaskState.Succeed,
            TaskResult = result
        };

        public static ApiTaskResult<TResult> Failure(Guid taskId, Urn taskType, ApiError apiError) => new()
        {
            Id = taskId,
            TaskType = taskType,
            TaskState = ApiTaskState.Failed,
            ApiError = apiError
        };

        public Guid Id { get; set; }
        public ApiTaskState TaskState { get; set; }
        public Urn TaskType { get; set; }
        public TResult TaskResult { get; set; }
        public ApiError ApiError { get; set; }

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
            if (TaskState == ApiTaskState.Succeed)
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
            if (TaskState == ApiTaskState.Succeed)
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
}