using System;
using System.Collections.Generic;
using ExternDotnetSDK.Errors;

namespace ExternDotnetSDK.Drafts.Check
{
    public class CheckResultData
    {
        public Dictionary<Guid, CheckError[]> DocumentsErrors { get; set; }
        public CheckError[] CommonErrors { get; set; }
    }
}