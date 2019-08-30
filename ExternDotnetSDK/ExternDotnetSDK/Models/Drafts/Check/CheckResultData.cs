using System;
using System.Collections.Generic;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Check
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CheckResultData
    {
        public Dictionary<Guid, CheckError[]> DocumentsErrors { get; set; }
        public CheckError[] CommonErrors { get; set; }
    }
}