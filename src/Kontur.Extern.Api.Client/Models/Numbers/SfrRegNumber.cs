using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Models.Numbers;

public record SfrRegNumber
{
    public static readonly RegexBasedParser<SfrRegNumber> Parser = new(@"^\d{3}-\d{3}-\d{6}$", v => new SfrRegNumber(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.PfrRegNumber, "ХХХ-ХХХ-ХХХХХХ"));

    /// <summary>
    /// Регистрационный номер документооборота СФР. Формат данных: ХХХ-ХХХ-ХХХХХХ, где Х - это цифра от 0 до 9
    /// </summary>
    public static SfrRegNumber Parse(string value) => Parser.Parse(value);

    internal SfrRegNumber(string value) => Value = value;

    public string Value { get; }
    public AuthorityNumberKind Kind => AuthorityNumberKind.SfrRegNumber;

    public override string ToString() => Value;
}