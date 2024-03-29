using Kontur.Extern.Api.Client.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Models.Numbers
{
    /// <summary>
    /// ИНН для ИП. Формат данных: 123456789012
    /// </summary>
    public record Inn
    {
        public static readonly RegexBasedParser<Inn> Parser = 
            new(@"^\d{12}$", v => new Inn(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Inn, "XXXXXXXXXXXX"));

        public static Inn Parse(string value) => Parser.Parse(value);

        internal Inn(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.Inn;

        public override string ToString() => Value;
    }
}