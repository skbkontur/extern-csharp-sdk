using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ExternDotnetSDK.Models.Common
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DateFormat : IsoDateTimeConverter
    {
        public DateFormat()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}