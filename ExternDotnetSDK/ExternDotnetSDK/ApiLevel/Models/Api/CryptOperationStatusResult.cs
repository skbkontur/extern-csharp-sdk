using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Api
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CryptOperationStatusResult
    {
        public OperationStatusInternal OperationStatus { get; set; }
        public IEnumerable<FileStatusInternal> FileStatuses { get; set; }
    }
}