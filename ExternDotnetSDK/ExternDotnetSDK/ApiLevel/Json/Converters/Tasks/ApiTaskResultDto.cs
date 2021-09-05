using System;
using Kontur.Extern.Client.Models.ApiTasks;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Errors;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.Tasks
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