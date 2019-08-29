using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KeApiClientOpenSdk.Models.Common
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DateFormat : IsoDateTimeConverter
    {
        public DateFormat() => DateTimeFormat = "yyyy-MM-dd";
    }
}