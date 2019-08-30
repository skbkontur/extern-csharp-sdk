using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Drafts.Check;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Prepare
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public interface IPrepareResult
    {
        CheckResultData CheckResult { get; set; }
        Link[] Links { get; set; }
        PrepareStatus Status { get; set; }
    }
}