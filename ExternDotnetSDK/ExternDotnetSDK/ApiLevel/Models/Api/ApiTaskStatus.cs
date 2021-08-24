using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Api
{
    [PublicAPI]
    public class ApiTaskStatus
    {
        public Guid Id { get; set; }
        public ApiTaskState TaskState { get; set; }
        public Urn TaskType { get; set; }
        public Link TaskResultLink { get; set; }
    }
}