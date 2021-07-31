using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// Код ИФНС. Формат данных: 1234
    /// </summary>
    public record IfnsCode
    {
        public static readonly RegexBasedParser<IfnsCode> Parser = new(
            @"^\d{4}$",
            v => new IfnsCode(v),
            (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.IfnsCode, "XXXX")
        );

        /// <summary>
        /// Формат данных: 1234
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IfnsCode Parse(string value) => Parser.Parse(value);

        private IfnsCode(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.IfnsCode;
        
        public override string ToString() => Value;
    }
}