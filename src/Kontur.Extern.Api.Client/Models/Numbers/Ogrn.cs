using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Models.Numbers;

/// <summary>
/// ОГРН - основной государственный регистрационный номер
/// </summary>
public record Ogrn
{
    /// <summary>
    /// ОГРНИП. Формат данных: 15-значный цифровой код
    /// </summary>
    public static readonly RegexBasedParser<Ogrn> IndividualEntrepreneur =
        new(@"^\d{15}$", v => new Ogrn(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Ogrn, "XXXXXXXXXXXXXXX"));

    /// <summary>
    /// ОГРН для ЮЛ. Формат данных: 13-значный цифровой код
    /// </summary>
    public static readonly RegexBasedParser<Ogrn> LegalEntity =
        new(@"^\d{13}$", v => new Ogrn(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Ogrn, "XXXXXXXXXXXXX"));

    public static Ogrn Parse(string value)
    {
        if (LegalEntity.TryParse(value, out var ogrn))
            return ogrn;

        if (IndividualEntrepreneur.TryParse(value, out ogrn))
            return ogrn;

        throw Errors.InvalidOgrn(nameof(value), value);
    }

    internal Ogrn(string value) => Value = value;

    public string Value { get; }
    public AuthorityNumberKind Kind => AuthorityNumberKind.Ogrn;

    public override string ToString() => Value;

}