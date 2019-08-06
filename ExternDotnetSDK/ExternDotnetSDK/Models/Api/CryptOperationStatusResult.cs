using System.Collections.Generic;
using ExternDotnetSDK.Models.Api.Enums;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Api
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CryptOperationStatusResult
    {
        public OperationStatusInternal OperationStatus { get; set; }
        public IEnumerable<FileStatusInternal> FileStatuses { get; set; }
    }
}