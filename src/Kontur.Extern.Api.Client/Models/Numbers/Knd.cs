using System.Diagnostics.CodeAnalysis;
using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Models.Numbers
{
    /// <summary>
    /// КНД – код налоговой декларации. Формат данных: семизначный цифровой код, где Х - это цифра от 0 до 9
    /// </summary>
    [SuppressMessage("ReSharper", "CommentTypo")]
    public record Knd
    {
        public static readonly RegexBasedParser<Knd> Parser = 
            new(@"^\d{7}$", v => new Knd(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Knd, "XXXXXXX"));

        /// <summary>
        /// Формат данных: семизначный цифровой код, где Х - это цифра от 0 до 9
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Knd Parse(string value) => Parser.Parse(value);

        internal Knd(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.Knd;
        
        public override string ToString() => Value;
    }
}