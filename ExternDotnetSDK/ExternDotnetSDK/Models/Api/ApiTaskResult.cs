using System;
using JetBrains.Annotations;
using KeApiClientOpenSdk.Models.Api.Enums;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.Errors;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Api
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class ApiTaskResult<TResult>
    {
        public static ApiTaskResult<TResult> Running(Guid taskId, Urn taskType) =>
            new ApiTaskResult<TResult>
            {
                Id = taskId,
                TaskState = ApiTaskState.Running,
                TaskType = taskType
            };

        public static ApiTaskResult<TResult> Success(Guid taskId, Urn taskType, TResult result) =>
            new ApiTaskResult<TResult>
            {
                Id = taskId,
                TaskType = taskType,
                TaskState = ApiTaskState.Succeed,
                TaskResult = result
            };

        public static ApiTaskResult<TResult> Failure(Guid taskId, Urn taskType, Error error) =>
            new ApiTaskResult<TResult>
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
    }
}