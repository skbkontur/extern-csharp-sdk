using System;
using System.Collections.Generic;
using ExternDotnetSDK.Errors;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts.Check
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class CheckResultData
    {
        public Dictionary<Guid, CheckError[]> DocumentsErrors { get; set; }
        public CheckError[] CommonErrors { get; set; }
    }
}