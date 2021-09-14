#nullable enable
using System;
using Kontur.Extern.Api.Client.Models.ApiErrors;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Tasks
{
    internal class ApiTaskResultDto
    {
        public Guid Id { get; set; }
        public ApiTaskState TaskState { get; set; }
        public Urn? TaskType { get; set; }
        public object? TaskResult { get; set; }
        public ApiError? ApiError { get; set; }
    }
}