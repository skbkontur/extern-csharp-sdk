using System;
using ExternDotnetSDK.Api.Enums;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Api
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class ApiTaskStatus
    {
        public Guid Id { get; set; }
        public ApiTaskState TaskState { get; set; }
        public Urn TaskType { get; set; }
        public Link TaskResultLink { get; set; }
    }
}