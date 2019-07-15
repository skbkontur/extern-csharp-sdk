using Newtonsoft.Json.Converters;

namespace ExternDotnetSDK.Documents
{
    public class DateFormat : IsoDateTimeConverter
    {
        public DateFormat()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}