using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts.Check;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Prepare
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PrepareResult : IPrepareResult
    {
        public CheckResultData CheckResult { get; set; }
        public Link[] Links { get; set; }
        public PrepareStatus Status { get; set; }
    }
}