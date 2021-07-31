using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// Код ПФР. Формат данных: ХХХ-ХХХ, где Х - это цифра от 0 до 9
    /// </summary>
    public record PfrCode
    {
        public static readonly RegexBasedParser<PfrCode> Parser = new(
            @"^\d{3}-\d{3}$",
            v => new PfrCode(v),
            (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.PfrCode, "ХХХ-ХХХ")
        );

        // <summary>
        /// Регистрационный номер документооборота ПФР. Формат данных: ХХХ-ХХХ, где Х - это цифра от 0 до 9
        /// </summary>
        public static PfrCode Parse(string value) => Parser.Parse(value);
        
        private PfrCode(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.PfrCode;
        
        public override string ToString() => Value;
    }
}