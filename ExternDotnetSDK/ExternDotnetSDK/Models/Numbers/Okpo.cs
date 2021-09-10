using Kontur.Extern.Client.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Models.Numbers
{
    /// <summary>
    /// ОКПО – общероссийский классификатор предприятий и организаций для ЮЛ и ИП.
    /// </summary>
    public record Okpo
    {
        /// <summary>
        /// ОКПО для ИП. Формат данных: десятизначный цифровой код
        /// </summary>
        public static readonly RegexBasedParser<Okpo> IndividualEntrepreneur =
            new(@"^\d{10}$", v => new Okpo(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Okpo, "XXXXXXXXXX"));

        /// <summary>
        /// ОКПО для ЮЛ. Формат данных: восьмизначный цифровой код
        /// </summary>
        public static readonly RegexBasedParser<Okpo> LegalEntity =
            new(@"^\d{8}$", v => new Okpo(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Okpo, "XXXXXXXX"));

        private Okpo(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.Okpo;
        
        public override string ToString() => Value;
    }
}