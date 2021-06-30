using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Check
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CheckResult
    {
        public CheckResultData Data { get; set; }
    }
}