using System;
using System.Collections.Generic;
using ExternDotnetSDK.Models.Errors;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Check
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CheckResultData
    {
        public Dictionary<Guid, CheckError[]> DocumentsErrors { get; set; }
        public CheckError[] CommonErrors { get; set; }
    }
}