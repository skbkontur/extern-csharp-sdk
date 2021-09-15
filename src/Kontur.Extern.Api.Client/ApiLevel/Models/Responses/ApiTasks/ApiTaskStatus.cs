using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.ApiTasks
{
    [PublicAPI]
    public class ApiTaskStatus
    {
        public Guid Id { get; set; }
        public ApiTaskState TaskState { get; set; }
        public Urn? TaskType { get; set; }
        public Link? TaskResultLink { get; set; }
    }
}