using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.NamingPolicies
{
    public static class StringCaseConvertExtensions
    {
        public static string KebabToCamelCase(this string value) => 
            string.Join("", value.Split('-')
                    .Select(word => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word)))
                .ToCamelCase();
        
        public static string ToKebabCase(this string value) => value.ToSeparatedCase('-');
        
        public static string ToSnakeCase(this string value) => value.ToSeparatedCase('_');

        private static string ToSeparatedCase(this string value, char separator)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var kebabCaseBuilder = new StringBuilder();
            var state = SeparatedCaseState.Start;

            for (var i = 0; i < value.Length; i++)
            {
                var currentChar = value[i];
                if (currentChar == ' ')
                {
                    if (state != SeparatedCaseState.Start)
                    {
                        state = SeparatedCaseState.NewWord;
                    }
                }
                else if (char.IsUpper(currentChar))
                {
                    switch (state)
                    {
                        case SeparatedCaseState.Upper:
                            var hasNext = i + 1 < value.Length;
                            if (i > 0 && hasNext)
                            {
                                var nextChar = value[i + 1];
                                if (!char.IsUpper(nextChar) && nextChar != separator)
                                {
                                    kebabCaseBuilder.Append(separator);
                                }
                            }
                            break;
                        case SeparatedCaseState.Lower:
                        case SeparatedCaseState.NewWord:
                            kebabCaseBuilder.Append(separator);
                            break;
                    }

                    kebabCaseBuilder.Append(char.ToLowerInvariant(currentChar));
                    state = SeparatedCaseState.Upper;
                }
                else if (currentChar == separator)
                {
                    kebabCaseBuilder.Append(separator);
                    state = SeparatedCaseState.Start;
                }
                else
                {
                    if (state == SeparatedCaseState.NewWord)
                        kebabCaseBuilder.Append(separator);

                    kebabCaseBuilder.Append(value[i]);
                    state = SeparatedCaseState.Lower;
                }
            }

            return kebabCaseBuilder.ToString();
        }
        
        private static string ToCamelCase(this string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length > 0 && char.IsUpper(value[0]))
                value = char.ToLower(value[0]) + value.Substring(1);
            return value;
        }
        
        private enum SeparatedCaseState
        {
            Start,
            Lower,
            Upper,
            NewWord
        }
    }
}