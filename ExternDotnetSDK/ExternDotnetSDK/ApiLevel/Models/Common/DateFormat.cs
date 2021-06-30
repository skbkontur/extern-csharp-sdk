using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DateFormat : IsoDateTimeConverter
    {
        public DateFormat() => DateTimeFormat = "yyyy-MM-dd";
    }
}