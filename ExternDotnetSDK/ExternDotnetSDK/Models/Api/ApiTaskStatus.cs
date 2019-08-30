using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Api.Enums;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Api
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class ApiTaskStatus
    {
        public Guid Id { get; set; }
        public ApiTaskState TaskState { get; set; }
        public Urn TaskType { get; set; }
        public Link TaskResultLink { get; set; }
    }
}