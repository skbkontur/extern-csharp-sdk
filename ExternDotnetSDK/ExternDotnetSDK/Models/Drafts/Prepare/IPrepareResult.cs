using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts.Check;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Drafts.Prepare
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public interface IPrepareResult
    {
        CheckResultData CheckResult { get; set; }
        Link[] Links { get; set; }
        PrepareStatus Status { get; set; }
    }
}