using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;

namespace Kontur.Extern.Client.ApiLevel.Models.Api
{
    public class CryptOperationStatusResult
    {
        public OperationStatusInternal OperationStatus { get; set; }
        public IEnumerable<FileStatusInternal> FileStatuses { get; set; }
    }
}