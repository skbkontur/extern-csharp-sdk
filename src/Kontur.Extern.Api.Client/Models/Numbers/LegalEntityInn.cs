using Kontur.Extern.Api.Client.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Models.Numbers
{
    /// <summary>
    /// ИНН для юрлиц. Формат данных: 1234567890
    /// </summary>
    public record LegalEntityInn
    {
        public static readonly RegexBasedParser<LegalEntityInn> Parser = 
            new(@"^\d{10}$", v => new LegalEntityInn(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.LegalEntityInn, "XXXXXXXXXX"));

        /// <summary>
        /// Формат данных: 1234567890
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static LegalEntityInn Parse(string value) => Parser.Parse(value);

        internal LegalEntityInn(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.LegalEntityInn;
        
        public override string ToString() => Value;
    }
}