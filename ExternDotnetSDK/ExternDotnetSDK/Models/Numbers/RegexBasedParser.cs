#nullable enable
using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Numbers
{
    [PublicAPI]
    public class RegexBasedParser<TValue>
    {
        private readonly Regex regex;
        private readonly Func<string, TValue> valueFactory;
        private readonly Func<string, string, Exception> errorFactory;

        public RegexBasedParser([RegexPattern] string regexPattern, Func<string, TValue> valueFactory, Func<string, string, Exception> errorFactory)
            : this(new Regex(regexPattern, RegexOptions.Compiled), valueFactory, errorFactory)
        {
        }

        public RegexBasedParser(Regex regex, Func<string, TValue> valueFactory, Func<string, string, Exception> errorFactory)
        {
            this.regex = regex;
            this.valueFactory = valueFactory;
            this.errorFactory = errorFactory;
        }

        public TValue Parse(string value) => 
            TryParse(value, out var parsedValue) 
                ? parsedValue 
                : throw errorFactory(nameof(value), value);

        public bool TryParse(string value, out TValue parsedValue)
        {
            if (regex.IsMatch(value))
            {
                parsedValue = valueFactory(value);
                return true;
            }

            parsedValue = default!;
            return false;
        }
    }
}