using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// Регистрационный номер документооборота ПФР. Формат данных: ХХХ-ХХХ-ХХХХХХ, где Х - это цифра от 0 до 9
    /// </summary>
    public record PfrRegNumber
    {
        public static readonly RegexBasedParser<PfrRegNumber> Parser = new(@"^\d{3}-\d{3}-\d{6}$", v => new PfrRegNumber(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.PfrRegNumber, "ХХХ-ХХХ-ХХХХХХ"));

        public static PfrRegNumber Parse(string value) => Parser.Parse(value);
        
        private PfrRegNumber(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.PfrRegNumber;
    }
}