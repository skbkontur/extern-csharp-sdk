using System.Collections.Generic;
using ExternDotnetSDK.Api.Enums;

namespace ExternDotnetSDK.Api
{
    public class CryptOperationStatusResult
    {
        public OperationStatusInternal OperationStatus { get; set; }
        public IEnumerable<FileStatusInternal> FileStatuses { get; set; }
    }
}