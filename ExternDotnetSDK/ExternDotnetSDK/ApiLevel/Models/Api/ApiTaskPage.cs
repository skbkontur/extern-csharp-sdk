using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Api
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class ApiTaskPage
    {
        public long Skip { get; set; }
        public long Take { get; set; }
        public long TotalCount { get; set; }
        public ApiTaskStatus[] ApiTaskPageItems { get; set; }
    }
}