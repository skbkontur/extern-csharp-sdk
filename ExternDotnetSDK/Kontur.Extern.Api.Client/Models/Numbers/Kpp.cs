using Kontur.Extern.Api.Client.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Models.Numbers
{
    /// <summary>
    /// КПП для юрлиц. Формат данных: 123456789
    /// </summary>
    public record Kpp
    {
        public static readonly RegexBasedParser<Kpp> Parser = new(
            @"^(\d{4})((\d|[A..Z]){2})(\d{3})$",
            v => new Kpp(v),
            (param, value) => Errors.InvalidKppAuthorityNumber(param, value)
        );

        /// <summary>
        /// Формат данных: 123456789
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Kpp Parse(string value) => Parser.Parse(value);

        private Kpp(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.Kpp;
        
        public override string ToString() => Value;
    }
}