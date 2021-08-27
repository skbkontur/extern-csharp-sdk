using Kontur.Extern.Client.Exceptions;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// Код ФСС. Формат данных: пятизначный цифровой код, последняя цифра 1 или 2
    /// </summary>
    public record FssCode
    {
        public static readonly RegexBasedParser<FssCode> Parser = new(
            @"^\d{4}[1..2]$",
            v => new FssCode(v),
            (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.FssCode, "XXXXX")
        );

        /// <summary>
        /// Пятизначный цифровой код, последняя цифра 1 или 2
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FssCode Parse(string value) => Parser.Parse(value);

        private FssCode(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.FssCode;
        
        public override string ToString() => Value;
    }
}