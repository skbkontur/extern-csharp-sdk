using ExternDotnetSDK.Common;
using ExternDotnetSDK.Drafts.Check;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts.Prepare
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public interface IPrepareResult
    {
        CheckResultData CheckResult { get; set; }
        Link[] Links { get; set; }
        PrepareStatus Status { get; set; }
    }
}