using System.Text;

namespace Kontur.Extern.Client.ApiLevel.Json.NamingStrategies
{
    internal static class StringCaseConvertExtensions
    {
        public static string ToKebabCase(this string value)
        {
            const char separator = '-';
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
        
        private enum SeparatedCaseState
        {
            Start,
            Lower,
            Upper,
            NewWord
        }
    }
}