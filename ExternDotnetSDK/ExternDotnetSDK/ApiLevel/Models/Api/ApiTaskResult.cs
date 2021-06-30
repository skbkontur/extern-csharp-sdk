using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Api
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