using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// КНД – код налоговой декларации. Формат данных: семизначный цифровой код, где Х - это цифра от 0 до 9
    /// </summary>
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

        private Knd(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.Knd;
    }
}