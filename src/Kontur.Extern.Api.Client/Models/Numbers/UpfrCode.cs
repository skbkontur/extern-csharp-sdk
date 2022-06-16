using Kontur.Extern.Api.Client.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Models.Numbers
{
    /// <summary>
    /// Код УПФР. Формат данных: ХХХ-ХХХ, где Х - это цифра от 0 до 9
    /// </summary>
    public record UpfrCode
    {
        public static readonly RegexBasedParser<UpfrCode> Parser = new(
            @"^\d{3}-\d{3}$",
            v => new UpfrCode(v),
            (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.UpfrCode, "ХХХ-ХХХ")
        );

        /// <summary>
        /// Код УПФР. Формат данных: ХХХ-ХХХ, где Х - это цифра от 0 до 9
        /// </summary>
        public static UpfrCode Parse(string value) => Parser.Parse(value);
        
        internal UpfrCode(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.UpfrCode;
        
        public override string ToString() => Value;
    }
}