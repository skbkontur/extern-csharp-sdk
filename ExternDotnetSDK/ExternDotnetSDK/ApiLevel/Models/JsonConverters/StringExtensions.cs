using System.Linq;

namespace Kontur.Extern.Client.ApiLevel.Models.JsonConverters
{
    internal static class StringExtensions
    {
        public static string ToKebabCase(this string str)
        {
            return string
                .Concat(
                    str.Select(
                        (x, i) => i > 0 && char.IsUpper(x)
                            ? "-" + x
                            : x.ToString()))
                .ToLower();
        }
    }
}