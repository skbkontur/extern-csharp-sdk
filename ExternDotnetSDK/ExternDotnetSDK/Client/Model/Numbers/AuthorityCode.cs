using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// Контролирующий орган. Формат данных: ФНС — ХХХХ, ПФР — ХХХ-ХХХ, ФСС — ХХХХХ, Росстат — ХХ-ХХ, где Х — это цифра от 0 до 9
    /// </summary>
    [PublicAPI]
    public record AuthorityCode
    {
        /// <summary>
        /// Контролирующий орган ФНС. Формат данных: ХХХХ, где Х — это цифра от 0 до 9
        /// </summary>
        public static readonly RegexBasedParser<AuthorityCode> Fns = CreateNumber(
            AuthorityNumberKind.FnsAuthorityCode,
            "ХХХХ",
            @"^\d{4}$" 
        );
        
        /// <summary>
        /// Контролирующий орган ПФР. Формат данных: ХХХ-ХХХ, где Х — это цифра от 0 до 9
        /// </summary>
        public static readonly RegexBasedParser<AuthorityCode> Pfr = CreateNumber(
            AuthorityNumberKind.PfrAuthorityCode,
            "ХХХ-ХХХ",
            @"^\d{3}-\d{3}$"
        );

        /// <summary>
        /// Контролирующий орган ФСС. Формат данных: ХХХХХ, где Х — это цифра от 0 до 9
        /// </summary>
        public static readonly RegexBasedParser<AuthorityCode> Fss = CreateNumber(
            AuthorityNumberKind.FssAuthorityCode,
            "ХХХХХ",
            @"^\d{5}$"
        );

        /// <summary>
        /// Контролирующий орган Росстат. Формат данных: ХХ-ХХ, где Х — это цифра от 0 до 9
        /// </summary>
        public static readonly RegexBasedParser<AuthorityCode> Rosstat = CreateNumber(
            AuthorityNumberKind.RosstatAuthorityCode,
            "ХХ-ХХ",
            @"^\d{2}-\d{2}$"
        );

        private AuthorityCode(string value, AuthorityNumberKind kind)
        {
            Value = value;
            Kind = kind;
        }

        public string Value { get; }
        public AuthorityNumberKind Kind { get; }
        
        public override string ToString() => Value;

        private static RegexBasedParser<AuthorityCode> CreateNumber(AuthorityNumberKind kind, string format, [RegexPattern] string regexPattern)
        {
            return new(
                new Regex(regexPattern, RegexOptions.Compiled),
                v => new AuthorityCode(v, kind),
                (param, value) => Errors.InvalidAuthorityNumber(param, value, kind, format) 
            );
        }
    }
}