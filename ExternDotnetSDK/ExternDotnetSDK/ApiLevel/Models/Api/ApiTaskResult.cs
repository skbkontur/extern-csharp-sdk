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

        public static ApiTaskResult<TResult> Failure(Guid taskId, Urn taskType, Error error) => new()
        {
            Id = taskId,
            TaskType = taskType,
            TaskState = ApiTaskState.Failed,
            Error = error
        };

        public Guid Id { get; set; }
        public ApiTaskState TaskState { get; set; }
        public Urn TaskType { get; set; }
        public TResult TaskResult { get; set; }
        public Error Error { get; set; }

        public ApiTaskResult<TOtherResult> Convert<TOtherResult>(Func<TResult, TOtherResult> converter) => new()
        {
            Error = Error,
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
            Error = apiTaskResult.Error;
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
            Error = apiTaskResult.Error;
            Id = apiTaskResult.Id;
            TaskState = apiTaskResult.TaskState;
            TaskType = apiTaskResult.TaskType;
            if (TaskState == ApiTaskState.Succeed)
            {
                TaskResult = apiTaskResult.TaskResult;
            }
        }

        public static implicit operator ApiTaskResult<TResult1, TResult2>(ApiTaskResult<TResult1> apiTaskResult) => new(apiTaskResult);
        
        public static implicit operator ApiTaskResult<TResult1, TResult2>(ApiTaskResult<TResult2> apiTaskResult) => new(apiTaskResult);
    }
}