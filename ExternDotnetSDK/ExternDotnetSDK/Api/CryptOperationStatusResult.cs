using System.Collections.Generic;
using ExternDotnetSDK.Api.Enums;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Api
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class CryptOperationStatusResult
    {
        public OperationStatusInternal OperationStatus { get; set; }
        public IEnumerable<FileStatusInternal> FileStatuses { get; set; }
    }
}