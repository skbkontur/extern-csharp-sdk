using Kontur.Extern.Api.Client.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Models.Numbers
{
    /// <summary>
    /// Код МРИ. Формат данных: 1234
    /// </summary>
    public record MriCode
    {
        public static readonly RegexBasedParser<MriCode> Parser = new(
            @"^\d{4}$",
            v => new MriCode(v),
            (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.MriCode, "XXXX")
        );

        /// <summary>
        /// Формат данных: 1234
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MriCode Parse(string value) => Parser.Parse(value);

        internal MriCode(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.MriCode;
        
        public override string ToString() => Value;
    }
}