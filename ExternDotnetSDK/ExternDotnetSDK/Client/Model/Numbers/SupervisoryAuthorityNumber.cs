using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Clients.Exceptions;

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// Контролирующий орган. Формат данных: ФНС — ХХХХ, ПФР — ХХХ-ХХХ, ФСС — ХХХХХ, Росстат — ХХ-ХХ, где Х — это цифра от 0 до 9
    /// </summary>
    [PublicAPI]
    public record SupervisoryAuthorityNumber
    {
        /// <summary>
        /// Контролирующий орган ФНС. Формат данных: ХХХХ, где Х — это цифра от 0 до 9
        /// </summary>
        public static readonly RegexBasedParser<SupervisoryAuthorityNumber> Fns = CreateNumber(
            AuthorityNumberKind.SupervisoryAuthorityFns,
            "ХХХХ",
            @"^\d{4}$" 
        );
        
        /// <summary>
        /// Контролирующий орган ПФР. Формат данных: ХХХ-ХХХ, где Х — это цифра от 0 до 9
        /// </summary>
        public static readonly RegexBasedParser<SupervisoryAuthorityNumber> Pfr = CreateNumber(
            AuthorityNumberKind.SupervisoryAuthorityPfr,
            "ХХХ-ХХХ",
            @"^\d{3}-\d{3}$"
        );

        /// <summary>
        /// Контролирующий орган ФСС. Формат данных: ХХХХХ, где Х — это цифра от 0 до 9
        /// </summary>
        public static readonly RegexBasedParser<SupervisoryAuthorityNumber> Fss = CreateNumber(
            AuthorityNumberKind.SupervisoryAuthorityFss,
            "ХХХХХ",
            @"^\d{5}$"
        );

        /// <summary>
        /// Контролирующий орган Росстат. Формат данных: ХХ-ХХ, где Х — это цифра от 0 до 9
        /// </summary>
        public static readonly RegexBasedParser<SupervisoryAuthorityNumber> Rosstat = CreateNumber(
            AuthorityNumberKind.SupervisoryAuthorityRosstat,
            "ХХ-ХХ",
            @"^\d{2}-\d{2}$"
        );

        private SupervisoryAuthorityNumber(string value, AuthorityNumberKind kind)
        {
            Value = value;
            Kind = kind;
        }

        public string Value { get; }
        public AuthorityNumberKind Kind { get; }

        private static RegexBasedParser<SupervisoryAuthorityNumber> CreateNumber(AuthorityNumberKind kind, string format, [RegexPattern] string regexPattern)
        {
            return new(
                new Regex(regexPattern, RegexOptions.Compiled),
                v => new SupervisoryAuthorityNumber(v, kind),
                (param, value) => Errors.InvalidAuthorityNumber(param, value, kind, format) 
            );
        }
    }
}