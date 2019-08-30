using System.Collections.Generic;
using Kontur.Extern.Client.Models.Api.Enums;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Api
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CryptOperationStatusResult
    {
        public OperationStatusInternal OperationStatus { get; set; }
        public IEnumerable<FileStatusInternal> FileStatuses { get; set; }
    }
}