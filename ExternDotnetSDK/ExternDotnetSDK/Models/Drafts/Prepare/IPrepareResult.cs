using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.Drafts.Check;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Drafts.Prepare
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public interface IPrepareResult
    {
        CheckResultData CheckResult { get; set; }
        Link[] Links { get; set; }
        PrepareStatus Status { get; set; }
    }
}