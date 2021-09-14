using Kontur.Extern.Api.Client.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Models.Numbers
{
    /// <summary>
    /// Регистрационный номер ФСС. Формат данных: 1234567890
    /// </summary>
    public record FssRegNumber
    {
        public static readonly RegexBasedParser<FssRegNumber> Parser = new(
            @"^(\d{10})$",
            v => new FssRegNumber(v),
            (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.FssRegNumber, "XXXXXXXXXX")
        );

        /// <summary>
        /// Формат данных: 123456789
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FssRegNumber Parse(string value) => Parser.Parse(value);

        private FssRegNumber(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.FssRegNumber;
        
        public override string ToString() => Value;
    }
}