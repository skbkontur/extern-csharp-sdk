using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Check;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Prepare
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PrepareResult : IPrepareResult
    {
        public CheckResultData CheckResult { get; set; }
        public Link[] Links { get; set; }
        public PrepareStatus Status { get; set; }
    }
}