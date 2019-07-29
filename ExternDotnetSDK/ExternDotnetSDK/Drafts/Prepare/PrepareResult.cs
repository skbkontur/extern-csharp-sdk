using ExternDotnetSDK.Common;
using ExternDotnetSDK.Drafts.Check;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts.Prepare
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PrepareResult : IPrepareResult
    {
        public CheckResultData CheckResult { get; set; }
        public Link[] Links { get; set; }
        public PrepareStatus Status { get; set; }
    }
}