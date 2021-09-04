using System.Text.Json;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.Extensions;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.Tasks
{
    internal class ApiTaskResultDtoPropNames
    {
        public ApiTaskResultDtoPropNames(JsonNamingPolicy namingPolicy)
        {
            TaskStatePropName = namingPolicy.ConvertName(nameof(ApiTaskResultDto.TaskState));
            TaskTypePropName = namingPolicy.ConvertName(nameof(ApiTaskResultDto.TaskType));
            IdPropName = namingPolicy.ConvertName(nameof(ApiTaskResultDto.Id));
            ApiErrorPropName = namingPolicy.ConvertName(nameof(ApiTaskResultDto.ApiError));
            TaskResultPropName = namingPolicy.ConvertName(nameof(ApiTaskResultDto.TaskResult));
        }

        public Utf8String TaskStatePropName { get; set; }
        public Utf8String TaskTypePropName { get; set; }
        public Utf8String IdPropName { get; set; }
        public Utf8String ApiErrorPropName { get; set; }
        public Utf8String TaskResultPropName { get; set; }
    }
}