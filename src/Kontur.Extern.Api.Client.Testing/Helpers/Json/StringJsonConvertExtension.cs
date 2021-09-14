using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kontur.Extern.Api.Client.Testing.Helpers.Json
{
    public static class StringJsonConvertExtension
    {
        public static string EllipsisLongStringValuesInJson(this string json, int ellipsisThreshold = 128)
        {
            var jToken = JToken.Parse(json);
            return jToken.ToString(Formatting.Indented, new LongValuesEllipsisConverter(ellipsisThreshold));
        }
    }
}