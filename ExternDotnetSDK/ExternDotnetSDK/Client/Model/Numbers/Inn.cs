using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// ИНН для ИП. Формат данных: 123456789012
    /// </summary>
    public record Inn
    {
        public static readonly RegexBasedParser<Inn> Parser = 
            new(@"^\d{12}$", v => new Inn(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Inn, "XXXXXXXXXXXX"));

        public static Inn Parse(string value) => Parser.Parse(value);

        private Inn(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.Inn;

        public override string ToString() => Value;
    }
}