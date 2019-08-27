using System.Linq;

namespace KeApiOpenSdk.Models.JsonConverters
{
    internal static class StringExtensions
    {
        public static string ToKebabCase(this string str)
        {
            return string
                .Concat(
                    str.Select(
                        (x, i) => i > 0 && char.IsUpper(x)
                            ? "-" + x.ToString()
                            : x.ToString()))
                .ToLower();
        }
    }
}