using Kontur.Extern.Api.Client.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Models.Numbers
{
    /// <summary>
    /// Регистрационный номер документооборота ПФР. Формат данных: ХХХ-ХХХ-ХХХХХХ, где Х - это цифра от 0 до 9
    /// </summary>
    public record PfrRegNumber
    {
        public static readonly RegexBasedParser<PfrRegNumber> Parser = new(@"^\d{3}-\d{3}-\d{6}$", v => new PfrRegNumber(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.PfrRegNumber, "ХХХ-ХХХ-ХХХХХХ"));

        /// <summary>
        /// Регистрационный номер документооборота ПФР. Формат данных: ХХХ-ХХХ-ХХХХХХ, где Х - это цифра от 0 до 9
        /// </summary>
        public static PfrRegNumber Parse(string value) => Parser.Parse(value);
        
        internal PfrRegNumber(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.PfrRegNumber;
        
        public override string ToString() => Value;
    }
}