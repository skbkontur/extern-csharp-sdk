using System;
using ExternDotnetSDK.Api.Enums;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.Errors;
using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Api
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class ApiTaskResult<TResult>
    {
        public static ApiTaskResult<TResult> Running(Guid taskId, Urn taskType)
        {
            return new ApiTaskResult<TResult>
            {
                Id = taskId,
                TaskState = ApiTaskState.Running,
                TaskType = taskType
            };
        }

        public static ApiTaskResult<TResult> Success(Guid taskId, Urn taskType, TResult result)
        {
            return new ApiTaskResult<TResult>
            {
                Id = taskId,
                TaskType = taskType,
                TaskState = ApiTaskState.Succeed,
                TaskResult = result
            };
        }

        public static ApiTaskResult<TResult> Failure(Guid taskId, Urn taskType, Error error)
        {
            return new ApiTaskResult<TResult>
            {
                Id = taskId,
                TaskType = taskType,
                TaskState = ApiTaskState.Failed,
                Error = error
            };
        }

        public Guid Id { get; set; }
        public ApiTaskState TaskState { get; set; }
        public Urn TaskType { get; set; }
        public TResult TaskResult { get; set; }
        public Error Error { get; set; }
    }
}