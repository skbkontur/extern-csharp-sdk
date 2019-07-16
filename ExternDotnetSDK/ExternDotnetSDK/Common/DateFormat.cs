using Newtonsoft.Json.Converters;

namespace ExternDotnetSDK.Common
{
    public class DateFormat : IsoDateTimeConverter
    {
        public DateFormat()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}