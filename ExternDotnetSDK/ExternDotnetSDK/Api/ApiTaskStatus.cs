using System;
using ExternDotnetSDK.Api.Enums;
using ExternDotnetSDK.Common;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Api
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