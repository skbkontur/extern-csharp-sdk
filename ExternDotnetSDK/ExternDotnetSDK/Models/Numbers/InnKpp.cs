using Kontur.Extern.Client.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Models.Numbers
{
    /// <summary>
    /// ИНН-КПП для юрлиц. Формат данных: 1234567890-123456789
    /// </summary>
    public record InnKpp
    {
        public static readonly RegexBasedParser<InnKpp> Parser = 
            new(@"^\d{10}-\d{9}$", v => new InnKpp(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.InnKpp, "XXXXXXXXXX-XXXXXXXXX"));

        /// <summary>
        /// Формат данных: 1234567890-123456789
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static InnKpp Parse(string value) => Parser.Parse(value);

        private InnKpp(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.InnKpp;
        
        public override string ToString() => Value;
    }
}