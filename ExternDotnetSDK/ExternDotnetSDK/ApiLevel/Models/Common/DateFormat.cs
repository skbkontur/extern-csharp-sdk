using Newtonsoft.Json.Converters;

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    public class DateFormat : IsoDateTimeConverter
    {
        public DateFormat() => DateTimeFormat = "yyyy-MM-dd";
    }
}