using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Check;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Prepare
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public interface IPrepareResult
    {
        CheckResultData CheckResult { get; set; }
        Link[] Links { get; set; }
        PrepareStatus Status { get; set; }
    }
}