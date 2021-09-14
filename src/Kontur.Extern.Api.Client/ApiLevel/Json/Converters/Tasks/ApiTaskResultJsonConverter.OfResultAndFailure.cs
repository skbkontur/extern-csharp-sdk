using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.ApiErrors;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Extensions;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Tasks
{
    internal partial class ApiTaskResultJsonConverter
    {
        private class ApiTaskResultOfTwoResultsJsonConverter<TResult, TFailureResult> : JsonConverter<ApiTaskResult<TResult, TFailureResult>>
            where TResult : IApiTaskResult
            where TFailureResult : IApiTaskResult
        {
            private readonly ApiTaskResultDtoPropNames propNames;

            public ApiTaskResultOfTwoResultsJsonConverter(ApiTaskResultDtoPropNames propNames) => this.propNames = propNames;

            public override ApiTaskResult<TResult, TFailureResult> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using var jsonDocument = JsonDocument.ParseValue(ref reader);
                var taskId = jsonDocument.RootElement.GetProperty(propNames.IdPropName.AsUtf8()).GetGuid();
                
                var taskStateValue = jsonDocument.RootElement.GetProperty(propNames.TaskStatePropName.AsUtf8()).GetString();
                var taskState = (ApiTaskState) Enum.Parse(typeof(ApiTaskState), taskStateValue!, true);
                
                var taskTypeValue = jsonDocument.RootElement.GetProperty(propNames.TaskTypePropName.AsUtf8()).GetString();
                var taskType = taskTypeValue == null ? null : Urn.Parse(taskTypeValue);

                switch (taskState)
                {
                    case ApiTaskState.Running:
                        return ApiTaskResult<TResult, TFailureResult>.Running(taskId, taskType);

                    case ApiTaskState.Succeed:
                        return DeserializeResult(jsonDocument, taskStateValue, taskId, taskType, options);

                    case ApiTaskState.Failed:
                        var errorJson = jsonDocument.RootElement.GetProperty(propNames.ApiErrorPropName.AsUtf8()).GetRawText();
                        var error = DeserializeAs<ApiError>(errorJson, propNames.ApiErrorPropName, taskStateValue, options);
                        return ApiTaskResult<TResult, TFailureResult>.TaskFailure(error, taskId, taskType);

                    default:
                        throw Errors.UnexpectedEnumMember(nameof(taskState), taskState);
                }
            }

            public override void Write(Utf8JsonWriter writer, ApiTaskResult<TResult, TFailureResult> value, JsonSerializerOptions options)
            {
                var dto = ToDto();
                if (value.TryGetTaskError(out var error))
                {
                    dto.ApiError = error;
                }
                else if (value.TryGetSuccessResult(out var successResult))
                {
                    dto.TaskResult = successResult;
                }
                else if (value.TryGetFailureResult(out var failureResult))
                {
                    dto.TaskState = ApiTaskState.Succeed;
                    dto.TaskResult = failureResult;
                }

                JsonSerializer.Serialize(writer, dto, options);

                ApiTaskResultDto ToDto()
                {
                    return new ApiTaskResultDto
                    {
                        Id = value.Id,
                        TaskState = value.TaskState,
                        TaskType = value.TaskType,
                    };
                }
            }
            
            private ApiTaskResult<TResult, TFailureResult> DeserializeResult(
                JsonDocument jsonDocument, 
                string? taskStateValue, 
                Guid taskId, 
                Urn? taskType, 
                JsonSerializerOptions? options)
            {
                var resultJson = jsonDocument.RootElement.GetProperty(propNames.TaskResultPropName.AsUtf8()).GetRawText();
                Exception? failureSerializationError = null;
                try
                {
                    var failureResult = DeserializeAs<TFailureResult>(resultJson, propNames.TaskResultPropName, taskStateValue, options);
                    if (!failureResult.IsEmpty)
                    {
                        return ApiTaskResult<TResult, TFailureResult>.FailureResult(failureResult, taskId, taskType);
                    }
                }
                catch (Exception ex)
                {
                    failureSerializationError = ex;
                }

                var successResult = DeserializeAs<TResult>(resultJson, propNames.TaskResultPropName, taskStateValue, options);
                if (successResult.IsEmpty && failureSerializationError is not null)
                    throw failureSerializationError;
                return ApiTaskResult<TResult, TFailureResult>.Success(successResult, taskId, taskType);
            }

            private T DeserializeAs<T>(string json, in Utf8String propName, string? taskStatePropValue, JsonSerializerOptions? options)
            {
                return JsonSerializer.Deserialize<T>(json, options) ??
                       throw Errors.JsonPropertyCannotBeNullIfAnotherPropertyHasValue(
                           propName.ToString(),
                           propNames.TaskStatePropName.ToString(),
                           taskStatePropValue);
            }
        }
    }
}