using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.Extensions;
using Kontur.Extern.Client.Models.ApiErrors;
using Kontur.Extern.Client.Models.ApiTasks;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.Tasks
{
    internal partial class ApiTaskResultJsonConverter
    {
        private class ApiTaskResultOfOneResultJsonConverter<TResult> : JsonConverter<ApiTaskResult<TResult>>
        {
            private readonly ApiTaskResultDtoPropNames propNames;

            public ApiTaskResultOfOneResultJsonConverter(ApiTaskResultDtoPropNames propNames) => this.propNames = propNames;

            public override ApiTaskResult<TResult> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using var jsonDocument = JsonDocument.ParseValue(ref reader);
                var taskId = jsonDocument.RootElement.GetProperty(propNames.IdPropName.AsUtf8()).GetGuid();
                
                var taskStateValue = jsonDocument.RootElement.GetProperty(propNames.TaskStatePropName.AsUtf8()).GetString();
                var taskState = (ApiTaskState) Enum.Parse(typeof(ApiTaskState), taskStateValue!, true);
                
                var taskTypeValue = jsonDocument.RootElement.GetProperty(propNames.TaskTypePropName.AsUtf8()).GetString();
                var taskType = taskTypeValue == null ? null : new Urn(taskTypeValue);

                switch (taskState)
                {
                    case ApiTaskState.Running:
                        return ApiTaskResult<TResult>.Running(taskId, taskType);

                    case ApiTaskState.Succeed:
                        var resultJson = jsonDocument.RootElement.GetProperty(propNames.TaskResultPropName.AsUtf8()).GetRawText();
                        var successResult = DeserializeAs<TResult>(resultJson, propNames.TaskResultPropName, taskStateValue);
                        return ApiTaskResult<TResult>.Success(successResult, taskId, taskType);

                    case ApiTaskState.Failed:
                        var errorJson = jsonDocument.RootElement.GetProperty(propNames.ApiErrorPropName.AsUtf8()).GetRawText();
                        var error = DeserializeAs<ApiError>(errorJson, propNames.ApiErrorPropName, taskStateValue);
                        return ApiTaskResult<TResult>.TaskFailure(error, taskId, taskType);

                    default:
                        throw Errors.UnexpectedEnumMember(nameof(taskState), taskState);
                }

                T DeserializeAs<T>(string json, in Utf8String propName, string? taskStatePropValue)
                {
                    return JsonSerializer.Deserialize<T>(json, options) ??
                           throw Errors.JsonPropertyCannotBeNullIfAnotherPropertyHasValue(
                               propName.ToString(),
                               propNames.TaskStatePropName.ToString(),
                               taskStatePropValue);
                }
            }

            public override void Write(Utf8JsonWriter writer, ApiTaskResult<TResult> value, JsonSerializerOptions options)
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
        }
    }
}