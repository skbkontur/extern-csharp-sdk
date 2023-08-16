using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Models.Numbers;

public record Snils
{
    /// <summary>
    /// Формат данных: 11-значный цифровой код
    /// </summary>
    private static readonly RegexBasedParser<Snils> PureDigits =
        new(@"^\d{11}$", v => new Snils(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Snils, "XXXXXXXXXXX"));

    /// <summary>
    /// Формат данных: ХХХ-ХХХ-ХХХ XX
    /// </summary>
    private static readonly RegexBasedParser<Snils> FormattedDigits =
        new(@"^\d{3}-\d{3}-\d{3} \d{2}$", v => new Snils(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Snils, "ХХХ-ХХХ-ХХХ XX"));

    public static Snils Parse(string value)
    {
        if (FormattedDigits.TryParse(value, out var snils))
            return snils;

        if (PureDigits.TryParse(value, out snils))
            return snils;

        throw Errors.InvalidSnils(nameof(value), value);
    }

    internal Snils(string value) => Value = value;

    public string Value { get; }
    public AuthorityNumberKind Kind => AuthorityNumberKind.Snils;

    public override string ToString() => Value;

}